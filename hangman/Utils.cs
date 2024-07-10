using System.Text.RegularExpressions;

class Utils
{
    public static string GetWordFromInput()
    {
        Console.WriteLine("Please enter a word:");
        string? input = Console.ReadLine();
        string match = "[a-zA-Z]+";
        Regex regex = new Regex(match);

        while (input == null || !regex.IsMatch(input))
        {
            Console.WriteLine("Invalid Input. Your input must consist only of upper or lower case letters. Please try again!");
            input = Console.ReadLine();
        }
        return input.ToLower();
    }

    public static char GetLetterFromInput()
    {
        Console.WriteLine("Please enter a letter:");
        string? input = Console.ReadLine();
        string match = "[a-z|A-Z]";
        Regex regex = new Regex(match);

        while (input == null || input.Length != 1 || !regex.IsMatch(input))
        {
            Console.WriteLine("Invalid Input. Your input must consist only of a single upper or lower case letter. Please try again!");
            input = Console.ReadLine();
        }
        return input.ToLower()[0];
    }

    public static string GenerateStringFromCorrectGuesses(string target, bool[] correctGuesses)
    {
        string str = "";
        for (int i = 0; i < target.Length; i++)
        {
            str += correctGuesses[i] ? target[i] + " " : "_ ";
        }
        return str;
    }

    public static string GetWordFromWorkBank()
    {
        //TODO
        return "";
    }


}