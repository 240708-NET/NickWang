using TetrisGame;

namespace CLIArgs
{

    class SeedArg : CommandLineArg
    {

        string seedString = "";

        public SeedArg(string flag) : base(flag) { }
        public override void ConsumeParameters(string[] args, ref int index)
        {
            try
            {
                seedString = args[++index];
            }
            catch (System.IndexOutOfRangeException)
            {
                throw new ParamsCountException("Incorrect number of parameters.");
            }
            catch (System.Exception)
            {
                throw new ParamsCountException("Unknown error when reading parameters.");
            }
        }

        public override string Description()
        {
            return $"   -{flag} <seed> a 32 bit integer to seed the random block picks. this argument should not be used with --load";
        }


        public override Task Act()
        {

            ProgramSettings.Seed = Int32.Parse(seedString);
            return Task.CompletedTask;
        }
    }
}