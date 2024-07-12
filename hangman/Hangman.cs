using System.Text;

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

    StringBuilder screenBuffer;

    string? target;
    int incorrectGuessCount = 0;
    int correctGuessCount = 0;
    bool[]? correctGuesses;
    SortedSet<char> incorrectGuessSet;
    SortedSet<char> correctGuessSet;
    List<char> availableGuesses;

    public Hangman()
    {
        screenBuffer = new StringBuilder();
        correctGuessSet = new SortedSet<char>();
        incorrectGuessSet = new SortedSet<char>();
        availableGuesses = Enumerable.Range(97, 26).Select(x => (char)x).ToList();
    }

    private void ReadArgs(string[] args)
    {
        if (args.Length == 0)
        {
            return;
        }
        string flag;
        for (int i = 0; i < args.Length; i++)
        {
            flag = args[i].Substring(2);
            if (!GameSettings.args.ContainsKey(flag))
            {
                throw new ArgsFormatException("Incorrect option. Use -h to see all options usage.");
            }

            CommandLineArg arg = GameSettings.args[flag];
            arg.ConsumeParameters(args, ref i);
            arg.Act();
        }
    }

    private void InitGame()
    {
        ClearDisplay();
        target = GameSettings.wordFetcher.FetchWord();
        correctGuesses = new bool[target.Length];
    }

    private bool PlayRound()
    {

        if (target == null || correctGuesses == null)
        {
            return false;
        }

        bool running = true;
        while (running)
        {
            ClearDisplay();
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

            char letter = Utils.GetLetterFromConsole();
            while (correctGuessSet.Contains(letter) || incorrectGuessSet.Contains(letter))
            {
                screenBuffer.AppendLine("You have already guessed this letter!");
                RenderBuffer();
                letter = Utils.GetLetterFromConsole();
            }

            if (availableGuesses[letter - 'a'] != 95)
            {
                availableGuesses[letter - 'a'] = (char)95;
            }

            bool correct = false;
            for (int i = 0; i < target.Length; i++)
            {
                if (target[i] == letter)
                {
                    correctGuesses[i] = true;
                    correct = true;
                    correctGuessCount++;
                    correctGuessSet.Add(letter);
                }
            }
            if (!correct)
            {
                incorrectGuessCount++;
                incorrectGuessSet.Add(letter);
            }

            if (incorrectGuessCount >= GameSettings.NUMBER_OF_GUESSES - 1)
            {
                screenBuffer.AppendLine("YOU LOST");
                screenBuffer.AppendLine($"Your word is {target}");
                running = false;
            }
            if (correctGuessCount >= target.Length)
            {
                screenBuffer.AppendLine("YOU WON");
                screenBuffer.AppendLine($"Your word is {target}");
                running = false;
            }

        }

        screenBuffer.Append("Would you like to continue?[Y/N]: ");
        RenderBuffer();

        return Utils.GetLetterFromConsole() == 'y';

    }

    private void ResetRound()
    {
        ClearDisplay();
        screenBuffer.Clear();
        incorrectGuessCount = 0;
        correctGuessCount = 0;
        correctGuessSet.Clear();
        incorrectGuessSet.Clear();
        availableGuesses = Enumerable.Range(97, 26).Select(x => (char)x).ToList();
        target = GameSettings.wordFetcher.FetchWord();
        correctGuesses = new bool[target.Length];
    }

    public void Run(string[] args)
    {
        try
        {
            ReadArgs(args);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            Environment.Exit(1);
        }
        catch (Exception)
        {
            Console.WriteLine("Unkown Exception.");
            Environment.Exit(1);
        }
        InitGame();
        while (PlayRound())
        {
            ResetRound();
        }
    }

    private void ClearDisplay()
    {
        if (GameSettings.disableClear) return;
        Console.Clear();
    }

    private void RenderBuffer()
    {
        Console.WriteLine(screenBuffer.ToString());
        screenBuffer.Clear();
    }

}
