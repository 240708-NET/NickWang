using CLIArgs;
using Fetcher;


/// <summary>
/// settigns of the program are set here. all fields here are static
/// CommandLineArgs modify program behavior here
/// </summary>
class GameSettings
{

    //max number of incorrect guesses before game is lost
    public static readonly int NUMBER_OF_GUESSES = 7;
    //container for all recongnized args
    public static readonly SortedDictionary<string, CommandLineArg> args;
    //fetcher for getting a target word
    public static WordFetcher wordFetcher;

    //the ascii art used to display the hangman
    public static string[] hangmanArt;

    //whether the answer is shown to the player
    public static bool enableCheats;
    //whether program is allowed to clear console
    public static bool disableClear;

    static GameSettings()
    {
        //an arg is accepted by the program if and only if they are added here 
        //the key and constructor arg should match. the string passed as key is 
        //what the program looks for when args are passed in. the string passed
        //into constructor is what's displayed by --help 
        //(this prob should be changed)
        args = new SortedDictionary<string, CommandLineArg>();
        args["art"] = new ArtSelectionArg("art");
        args["cheat"] = new CheatArg("cheat");
        args["help"] = new HelpArg("help");
        args["source"] = new SourceArg("source");
        args["disableClearDisplay"] = new ClearScreenArg("disableClearDisplay");
        //default fetcher is from console
        wordFetcher = new ConsoleWordFetcher();
        //default ascii art is art 1
        hangmanArt = DisplayConstants.HANGMAN_ART_1;
        //cheats are by default disabled
        enableCheats = false;
        //clearing is by default allowed
        disableClear = false;
    }

}