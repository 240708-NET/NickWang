using CLIArgs;

public class Program
{
    public static void Main(string[] args)
    {
        ReadArgs(args);
        ProgramSettings.Tetris.SetSeed(ProgramSettings.Seed);
        ProgramSettings.Tetris.Run();
    }

    private static void ReadArgs(string[] args)
    {
        //returns if no args are provided
        if (args.Length == 0)
        {
            return;
        }
        string flag;
        for (int i = 0; i < args.Length; i++)
        {
            //flags should starts with "--". But it is not checked
            flag = args[i].Substring(2);
            if (!ProgramSettings.Args.ContainsKey(flag))
            {
                throw new ArgsFormatException("Incorrect option. Use --help to see all options usage.");
            }
            //CommandLineArg objects are retrieved from ProgramSettings.cs 
            CommandLineArg arg = ProgramSettings.Args[flag];
            //parameters to args are consumed here
            arg.ConsumeParameters(args, ref i);
            //args effect on program is defined in this method
            arg.Act();
        }
    }
}
