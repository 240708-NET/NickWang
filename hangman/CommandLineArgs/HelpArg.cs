using HangmanExceptions;

namespace CLIArgs
{
    /// <summary>
    /// arg to see all available args
    /// </summary>
    class HelpArg : CommandLineArg
    {
        public HelpArg(string flag) : base(flag) { }

        /// <summary>
        /// does not require any additional parameter
        /// </summary>
        /// <param name="args"></param>
        /// <param name="index"></param>
        /// <exception cref="ParamsReadException"></exception>
        public override void ConsumeParameters(string[] args, ref int index)
        {
            try
            {

            }
            catch (System.Exception)
            {
                throw new ParamsReadException("Unknown error when reading parameters.");
            }
        }

        public override string Description()
        {
            return $"   --{flag} list the available args";
        }


        /// <summary>
        /// display all available args and their usage. <> indicate required parameters. [] indicate optional parameters.
        /// program exits after info is displayed
        /// </summary>
        public override void Act()
        {
            Console.WriteLine("usage: hangman [<flag> [params]]");
            foreach (var arg in GameSettings.args)
            {
                Console.WriteLine(arg.Value.Description());
            }
            Environment.Exit(0);
        }
    }
}