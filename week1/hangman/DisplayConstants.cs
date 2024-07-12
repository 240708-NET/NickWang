/// <summary>
/// constants involved with display. 
/// currently only contains the 2 ascii hangman arts
/// all display messages may be moved to this file in the future
/// </summary>
class DisplayConstants
{
    public static readonly string[] HANGMAN_ART_1 = {
                                    "  +---+\n  |   |\n      |\n      |\n      |\n      |\n=========",
                                    "  +---+\n  |   |\n  O   |\n      |\n      |\n      |\n=========",
                                    "  +---+\n  |   |\n  O   |\n  |   |\n      |\n      |\n=========",
                                    "  +---+\n  |   |\n  O   |\n /|   |\n      |\n      |\n=========",
                                    "  +---+\n  |   |\n  O   |\n /|\\  |\n      |\n      |\n=========",
                                    "  +---+\n  |   |\n  O   |\n /|\\  |\n /    |\n      |\n=========",
                                    "  +---+\n  |   |\n  O   |\n /|\\  |\n / \\  |\n      |\n========="};

    // :( the format is terrible but whatever
    public static readonly string[] HANGMAN_ART_2 = {@" ____
|/   |
|   
|    
|    
|    
|
|_____
",
@" ____
|/   |
|   (_)
|    
|    
|    
|
|_____
",
@" ____
|/   |
|   (_)
|    |
|    |    
|    
|
|_____
",
@" ____
|/   |
|   (_)
|   \|
|    |
|    
|
|_____
",
@" ____
|/   |
|   (_)
|   \|/
|    |
|    
|
|_____
",
@" ____
|/   |
|   (_)
|   \|/
|    |
|   / 
|
|_____
",
@" ____
|/   |
|   (_)
|   \|/
|    |
|   / \
|
|_____
",
@" ____
|/   |
|   (_)
|   /|\
|    |
|   | |
|
|_____"};
}