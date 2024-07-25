using CLIArgs;

class ProgramSettings
{
    public static int Seed;
    public static Tetris Tetris;
    public static int PlayerId;
    public static string PlayerName = "";
    public static readonly SortedDictionary<string, CommandLineArg> Args;

    public static DataContext DataContext;
    static ProgramSettings()
    {
        //an arg is accepted by the program if and only if they are added here 
        //the key and constructor arg should match. the string passed as key is 
        //what the program looks for when args are passed in. the string passed
        //into constructor is what's displayed by --help 
        //(this prob should be changed)
        Tetris = new Tetris();
        Seed = Guid.NewGuid().GetHashCode();
        Args = new SortedDictionary<string, CommandLineArg>();
        Args["help"] = new HelpArg("help");
        Args["load"] = new LoadSaveArg("load");
        Args["seed"] = new SeedArg("seed");
        Args["name"] = new NewUserArg("name");
        Args["login"] = new LoginArg("login");
        DataContext = new DataContext();
    }
}