
using System.Text;

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
            flag = args[i].Substring(1);
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
        Console.Clear();
    }

    private void RenderBuffer()
    {
        Console.WriteLine(screenBuffer.ToString());
        screenBuffer.Clear();
    }

}
