
using System.Text;

class Hangman
{
    static readonly int NUMBER_OF_GUESSES = 7;
    static readonly string[] HANGMAN_ART =  {
                                    "  +---+\n  |   |\n      |\n      |\n      |\n      |\n=========",
                                    "  +---+\n  |   |\n  O   |\n      |\n      |\n      |\n=========",
                                    "  +---+\n  |   |\n  O   |\n  |   |\n      |\n      |\n=========",
                                    "  +---+\n  |   |\n  O   |\n /|   |\n      |\n      |\n=========",
                                    "  +---+\n  |   |\n  O   |\n /|\\  |\n      |\n      |\n=========",
                                    "  +---+\n  |   |\n  O   |\n /|\\  |\n /    |\n      |\n=========",
                                    "  +---+\n  |   |\n  O   |\n /|\\  |\n / \\  |\n      |\n========="};

    IWordFetcher wf;
    StringBuilder screenBuffer;

    string target = "";
    int incorrectGuessCount = 0;
    int correctGuessCount = 0;
    bool[]? correctGuesses;
    SortedSet<char> correctGuessSet;

    public Hangman()
    {
        screenBuffer = new StringBuilder();
        correctGuessSet = new SortedSet<char>();
    }

    private void InitGame(string[] args)
    {
        ClearDisplay();
        wf = new WebWordFetcher();
        target = wf.FetchWord();
        correctGuesses = new bool[target.Length];
    }

    private bool PlayRound(string[] args)
    {

        while (true)
        {
            ClearDisplay();
            screenBuffer.AppendLine($"Your current guess is: {Utils.GenerateStringFromCorrectGuesses(target, correctGuesses)}");
            screenBuffer.AppendLine("You have already guessed: " + string.Join(", ", correctGuessSet));
            screenBuffer.AppendLine(HANGMAN_ART[incorrectGuessCount]);
            RenderBuffer();

            char letter = Utils.GetLetterFromConsole();
            if (correctGuessSet.Contains(letter))
            {
                screenBuffer.AppendLine("You have already guessed this letter!");
                RenderBuffer();
                continue;
            }
            correctGuessSet.Add(letter);

            bool correct = false;
            for (int i = 0; i < target.Length; i++)
            {
                if (target[i] == letter)
                {
                    correctGuesses[i] = true;
                    correct = true;
                    correctGuessCount++;
                }
            }
            if (!correct)
            {
                incorrectGuessCount++;
            }

            if (incorrectGuessCount >= NUMBER_OF_GUESSES - 1)
            {
                screenBuffer.AppendLine("YOU LOST");
                screenBuffer.AppendLine($"Your word is {target}");
                break;
            }
            if (correctGuessCount >= target.Length)
            {
                screenBuffer.AppendLine("YOU WON");
                screenBuffer.AppendLine($"Your word is {target}");
                break;
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
        target = wf.FetchWord();
        correctGuesses = new bool[target.Length];
    }

    public void Run(string[] args)
    {
        InitGame(args);
        while (PlayRound(args))
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
