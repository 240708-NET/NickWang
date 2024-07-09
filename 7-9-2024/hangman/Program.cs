using System.Text.RegularExpressions;

class Program
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

    // static string[] HANGMAN_WORDS = {}

    public static void Main(string[] args)
    {
        string target = GetWordFromInput();
        Write($"The word to guess is {target}.");
        
        int wrongCount = 0;
        int correctCount = 0;
        bool[] correctGuesses = new bool[target.Length];
        SortedSet<char> correctGuessSet= new SortedSet<char>();

        while (true){
            Write($"Your current guess is: {GenerateStringFromCorrectGuesses(target, correctGuesses)}");
            Write("You have already guessed: " + string.Join(", ", correctGuessSet));
            Write(HANGMAN_ART[wrongCount]);

            char letter = GetLetterFromInput();
            if(correctGuessSet.Contains(letter)){
                Write("You have already guessed this letter!");
                continue;
            }
            correctGuessSet.Add(letter);

            bool correct = false;
            for (int i = 0; i < target.Length; i++)
            {
                if(target[i] == letter){
                    correctGuesses[i] = true;
                    correct = true;
                    correctCount++;
                }
            }
            if(!correct){
                wrongCount++;
            }

            if(wrongCount >= NUMBER_OF_GUESSES-1){
                Write("YOU LOST");
                break;
            }
            if(correctCount >= target.Length){
                Write("YOU WON");
                Write($"Your word is {target}") ;
                break;
            }
        }


    }

    public static string GetWordFromInput()
    {
        Write("Please enter a word:");
        string? input = Console.ReadLine();
        string match = "[a-zA-Z]+";
        Regex regex = new Regex(match);

        while (input == null || !regex.IsMatch(input))
        {
            Write("Invalid Input. Please try again!");
            input = Console.ReadLine();
        }
        return input.ToLower();
    }

    public static char GetLetterFromInput()
    {
        Write("Please enter a letter:");
        string? input = Console.ReadLine();
        string match = "[a-z|A-Z]";
        Regex regex = new Regex(match);

        while (input == null || !regex.IsMatch(input))
        {
            Write("Invalid Input. Please try again!");
            input = Console.ReadLine();
        }
        return input.ToLower()[0];
    }

    public static string GenerateStringFromCorrectGuesses(string target, bool[] correctGuesses){
        string str = "";
        for (int i = 0; i < target.Length; i++)
        {
            str += correctGuesses[i] ? target[i]+" " : "_ ";
        }
        return str;
    }

    public static string GetWordFromWorkBank(){
        //TODO
        return "";
    }

    public static void Write(string str)
    {
        Console.WriteLine(str);
    }
}