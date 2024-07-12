class GameSettings
{

    public static readonly int NUMBER_OF_GUESSES = 7;
    public static readonly SortedDictionary<string, CommandLineArg> args;
    public static IWordFetcher wordFetcher;

    public static string[] hangmanArt;

    public static bool enableCheats;
    public static bool disableClear;

    static GameSettings()
    {
        args = new SortedDictionary<string, CommandLineArg>();
        args["art"] = new ArtSelectionArg("art");
        args["cheat"] = new CheatArg("cheat");
        args["help"] = new HelpArg("help");
        args["source"] = new SourceArg("source");
        args["disableClearDisplay"] = new ClearScreenArg("disableClearDisplay");
        wordFetcher = new ConsoleWordFetcher();
        hangmanArt = DisplayConstants.HANGMAN_ART_1;
        enableCheats = false;
        disableClear = false; ;
    }

}