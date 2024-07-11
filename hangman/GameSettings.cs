class GameSettings
{

    public static readonly int NUMBER_OF_GUESSES = 7;
    public static readonly Dictionary<string, CommandLineArg> args;
    public static IWordFetcher wordFetcher;

    public static string[] hangmanArt;

    static GameSettings()
    {
        args = new Dictionary<string, CommandLineArg>();
        args["s"] = new SourceArg("s");
        args["h"] = new HelpArg("h");
        args["a"] = new ArtSelectionArg("a");
        wordFetcher = new ConsoleWordFetcher();
        hangmanArt = DisplayConstants.HANGMAN_ART_1;
    }

}