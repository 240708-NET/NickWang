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

    string target;

    public Hangman()
    {

    }

    public void Run()
    {
        Console.Clear();
        string target = Utils.GetWordFromInput();
        Display($"The word to guess is {target}.");

        int incorrectGuessCount = 0;
        int correctGuessCount = 0;
        bool[] correctGuesses = new bool[target.Length];
        SortedSet<char> correctGuessSet = new SortedSet<char>();

        while (true)
        {
            Console.Clear();
            Display($"Your current guess is: {Utils.GenerateStringFromCorrectGuesses(target, correctGuesses)}");
            Display("You have already guessed: " + string.Join(", ", correctGuessSet));
            Display(HANGMAN_ART[incorrectGuessCount]);

            char letter = Utils.GetLetterFromInput();
            if (correctGuessSet.Contains(letter))
            {
                Display("You have already guessed this letter!");
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
                Display("YOU LOST");
                Display($"Your word is {target}");
                break;
            }
            if (correctGuessCount >= target.Length)
            {
                Display("YOU WON");
                Display($"Your word is {target}");
                break;
            }
        }

    }

    private void Display(string str)
    {
        Console.WriteLine(str);
    }

}