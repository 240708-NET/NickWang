using System.Text;
using CLIArgs;
using HangmanExceptions;

/*
This program is a command line hangman game. 
The user has the option to pass in arguments to alter the behavior of the program.
The available options are --help, --source to control the source of the word choice,
--cheats to enable cheats, --art to pick the ascii art style.

When the program is launched it first fetches a word either through CLI(default) or 
an API call configured through program args. A failed API call will default the program 
back to getting the word from CLI. User input through CLI is checked. An invalid input 
should result in a repeated prompt.

Once a word choice is obtained, the game begins and the user is shown the following things:
    - the current progres. this is what letters and position the user has guessed correctly 
    - the correct guesses. letters that have been correctly guessed displayed in sorted order
    - the incorrect guesses. letters that have been incorrectly guessed displayed in sorted order
    - the available guesses. letters that have not been guessed. already guessed letters are not removed from this list but replaced with '_'
    - an ascii art of the hangman representing the progress of the game

The user is repeatedly prompted for guesses, then that guess is processed, 
until either the user wins or losses. When the user is prompted a letter, they will 
have to enter a single a-z letter in either lower or upper case. If the input is 
invalid, the user is repeatedly prompted until a valid input is provided. If a 
letter is valid but has been guessed, the user is prompted again. Then the 
information on screen is updated to reflect the new guess. If a correct letter 
is guessed, the "current guess" displays the new guess in the correct position 
in the word. "correct guesses" and "incorrect guesses" now show the new guess. 
"available guesses" removes the new guess. Ascii hangman art should be updated 
to reflect the new guess.

Once the user either wins or losses, they are prompted to play agin. The response
should be a single lower/upper case letter 'Y' for yes and everything else for no.
*/
class Hangman
{

    //buffer to hold display updates before they are all at once written to console
    StringBuilder screenBuffer;

    //the word to guess
    string? target;
    //number of unique incorrect guesses
    int incorrectGuessCount = 0;
    //number of correct(non-unique) guesses
    int correctGuessCount = 0;
    //keeps track of the correct letters in the target word that has been guessed correctly
    //false for not guessed true for guessed
    bool[]? correctGuesses;
    //tracks incorrect guesses
    SortedSet<char> incorrectGuessSet;
    //tracks correct guesses
    SortedSet<char> correctGuessSet;
    //list of available guesses. Elements are never remved for this list. When a guess is made it is replaced with '_'
    List<char> availableGuesses;

    public Hangman()
    {
        screenBuffer = new StringBuilder();
        correctGuessSet = new SortedSet<char>();
        incorrectGuessSet = new SortedSet<char>();
        //init the availableGuess to be letters a-z lower case.        
        availableGuesses = Enumerable.Range(97, 26).Select(x => (char)x).ToList();
    }

    /// <summary>
    /// Read command line args if any exists. Additional exceptions may be thrown from the arg.ConsumeParameters call
    /// </summary>
    /// <param name="args">string array of args</param>
    /// <exception cref="ArgsFormatException">this exception is thrown if flags cannot be read correctly</exception>
    private void ReadArgs(string[] args)
    {
        //returns if no args are provided
        if (args.Length == 0)
        {
            return;
        }
        string flag;
        for (int i = 0; i < args.Length; i++)
        {
            //flags should starts with "--". But it is not checked
            flag = args[i].Substring(2);
            if (!GameSettings.args.ContainsKey(flag))
            {
                throw new ArgsFormatException("Incorrect option. Use --help to see all options usage.");
            }
            //CommandLineArg objects are retrieved from GameSettings 
            CommandLineArg arg = GameSettings.args[flag];
            //parameters to args are consumed here
            arg.ConsumeParameters(args, ref i);
            //args effect on program is defined in this method
            arg.Act();
        }
    }

    /// <summary>
    /// clears display, fetch word choice, then init anything that waits on target word 
    /// </summary>
    private void InitGame()
    {
        ClearDisplay();
        target = GameSettings.wordFetcher.FetchWord();
        correctGuesses = new bool[target.Length];
    }

    /// <summary>
    /// game logic involed with 1 complete game of hangman. prompts the user for another game after 1 ends
    /// </summary>
    /// <returns>true if the user wishes to play another round</returns>
    private bool PlayRound()
    {
        //if target word is somehow not retrieved, return false and ends game
        if (target == null || correctGuesses == null)
        {
            return false;
        }

        bool running = true;
        while (running)
        {
            ClearDisplay();
            //add relavent info to display buffer
            if (GameSettings.enableCheats)
            {
                screenBuffer.AppendLine($"The answer is: {target}");
            }
            screenBuffer.AppendLine($"Your current guess is: {Utils.GenerateStringFromCorrectGuesses(target, correctGuesses)}");
            screenBuffer.AppendLine("Your correct guesses are: " + string.Join(", ", correctGuessSet));
            screenBuffer.AppendLine("Your incorrect guesses are: " + string.Join(", ", incorrectGuessSet));
            screenBuffer.AppendLine("Your available guesses are: " + string.Join(", ", availableGuesses));
            screenBuffer.AppendLine(GameSettings.hangmanArt[incorrectGuessCount]);
            RenderBuffer();

            //fetch user input letter
            char letter = Utils.GetLetterFromConsole();
            //check if letter has been guessed
            while (correctGuessSet.Contains(letter) || incorrectGuessSet.Contains(letter))
            {
                screenBuffer.AppendLine("You have already guessed this letter!");
                RenderBuffer();
                //prompt again if letter has been guessed
                letter = Utils.GetLetterFromConsole();
            }

            //update availabe guesses 
            if (availableGuesses[letter - 'a'] != 95)
            {
                availableGuesses[letter - 'a'] = (char)95;
            }

            //check if letter guess is present in target word
            bool correct = false;
            for (int i = 0; i < target.Length; i++)
            {
                if (target[i] == letter)
                {
                    correctGuesses[i] = true;
                    correct = true;
                    //correctGuessCount reflect every correct(may not be uniue) letter guesses
                    //i.e. apple with guess p increase correctGuessCount by 2
                    correctGuessCount++;
                    correctGuessSet.Add(letter);
                }
            }

            //if letter guess is incorrect
            if (!correct)
            {
                //incorrectGuessCount reflect unique incorrect letter guess
                incorrectGuessCount++;
                incorrectGuessSet.Add(letter);
            }

            //check if game lost
            if (incorrectGuessCount >= GameSettings.NUMBER_OF_GUESSES - 1)
            {
                screenBuffer.AppendLine("YOU LOST");
                screenBuffer.AppendLine($"Your word is {target}");
                running = false;
            }
            //check if game won
            if (correctGuessCount >= target.Length)
            {
                screenBuffer.AppendLine("YOU WON");
                screenBuffer.AppendLine($"Your word is {target}");
                running = false;
            }

        }

        //prompt user for another game
        screenBuffer.Append("Would you like to continue?[Y/N]: ");
        RenderBuffer();

        //returns true if input is 'Y' or 'y' and false for anything else
        return Utils.GetLetterFromConsole() == 'y';

    }

    /// <summary>
    /// resets everything needed to start another game
    /// </summary>
    private void ResetRound()
    {
        ClearDisplay();
        //clear any existing text in screenBuffer
        screenBuffer.Clear();
        incorrectGuessCount = 0;
        correctGuessCount = 0;
        //reset correct and incorrect guess set to empty
        correctGuessSet.Clear();
        incorrectGuessSet.Clear();
        //availableGuesses reset to a-z lower case
        availableGuesses = Enumerable.Range(97, 26).Select(x => (char)x).ToList();
        //fetch another word
        target = GameSettings.wordFetcher.FetchWord();
        //reset correctGuesses to all false
        correctGuesses = new bool[target.Length];
    }

    /// <summary>
    /// runner for hangman game
    /// </summary>
    /// <param name="args">command line args</param>
    public void Run(string[] args)
    {
        try
        {
            //try reading args. Exceptions may be thrown when reading args
            ReadArgs(args);
        }
        catch (HangmanExceptions.ArgumentException e)
        {
            //all Exceptions related to reading args
            Console.WriteLine(e.Message);
            Environment.Exit(1);
        }
        catch (Exception)
        {
            //any other exceptions
            Console.WriteLine("Unkown Exception.");
            Environment.Exit(1);
        }
        //init and start game
        InitGame();
        while (PlayRound())
        {
            ResetRound();
        }
    }

    /// <summary>
    /// clear display if setting allows
    /// </summary>
    private void ClearDisplay()
    {
        if (GameSettings.disableClear) return;
        Console.Clear();
    }

    /// <summary>
    /// render whats in screenBuffer and clears buffer
    /// </summary>
    private void RenderBuffer()
    {
        Console.WriteLine(screenBuffer.ToString());
        screenBuffer.Clear();
    }

}
