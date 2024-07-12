using System.Text.RegularExpressions;

class Utils
{
    /// <summary>
    /// fetch a word from console
    /// if input is invalid repeated trying until a valid input is given
    /// </summary>
    /// <returns>a string containing only lower case letters</returns>
    public static string GetWordFromConsole()
    {
        //user can input uppwer or lower case word here
        Console.WriteLine("Please enter a word:");
        string? input = Console.ReadLine();
        string match = "[a-zA-Z]+";
        Regex regex = new Regex(match);

        //validate with regex and try again if needed
        while (input == null || !regex.IsMatch(input))
        {
            Console.WriteLine("Invalid Input. Your input must consist only of upper or lower case letters. Please try again!");
            input = Console.ReadLine();
        }
        //return only lower case word
        return input.ToLower();
    }

    /// <summary>
    /// fetch a letter from console
    /// if input is invalid repeated trying until a valid input is given
    /// </summary>
    /// <returns>a lower case char</returns>
    public static char GetLetterFromConsole()
    {
        //user can input uppwer or lower case letter here
        Console.WriteLine("Please enter a letter:");
        string? input = Console.ReadLine();
        string match = "[a-z|A-Z]";
        Regex regex = new Regex(match);

        //validate with regex and try again if needed
        while (input == null || input.Length != 1 || !regex.IsMatch(input))
        {
            Console.WriteLine("Invalid Input. Your input must consist only of a single upper or lower case letter. Please try again!");
            input = Console.ReadLine();
        }
        //return only lower case char
        return input.ToLower()[0];
    }

    /// <summary>
    /// formats a string to display the target word guesses and progress
    /// i.e. _ p p _ _ for apple
    /// </summary>
    /// <param name="target">the target word</param>
    /// <param name="correctGuesses">the correctGuesses array</param>
    /// <returns>a string consisting of '_' for unguessed spaces and the corresponding letter for correctly guessed spaces</returns>
    public static string GenerateStringFromCorrectGuesses(string target, bool[] correctGuesses)
    {
        string str = "";
        for (int i = 0; i < target.Length; i++)
        {
            str += correctGuesses[i] ? target[i] + " " : "_ ";
        }
        return str;
    }
}