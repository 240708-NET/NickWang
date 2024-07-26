using System.Net.Http.Json;
using System.Text.Json;
using CLIArgs;

namespace TetrisGame;
public class Program
{
    public static async Task Main(string[] args)
    {
        await ReadArgs(args);
        ProgramSettings.Tetris.SetSeed(ProgramSettings.Seed);
        await ProgramSettings.Tetris.Run();
    }

    private static async Task ReadArgs(string[] args)
    {
        //returns if no args are provided
        if (args.Length == 0)
        {
            return;
        }
        string flag;
        for (int i = 0; i < args.Length; i++)
        {
            //flags should starts with "-". But it is not checked
            flag = args[i].Substring(1);
            if (!ProgramSettings.Args.ContainsKey(flag))
            {
                throw new ArgsFormatException("Incorrect option. Use --help to see all options usage.");
            }
            //CommandLineArg objects are retrieved from ProgramSettings.cs 
            CommandLineArg arg = ProgramSettings.Args[flag];
            //parameters to args are consumed here
            arg.ConsumeParameters(args, ref i);
            //args effect on program is defined in this method
            await arg.Act();
        }
    }
}
