using TetrisGame;

namespace CLIArgs
{

    class LoadSaveArg : CommandLineArg
    {

        string path = "";

        public LoadSaveArg(string flag) : base(flag) { }

        public override void ConsumeParameters(string[] args, ref int index)
        {
            try
            {
                path = args[++index];
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
            return $"   -{flag} <path> provides a relative path to a save file. when loading save no other arguments should be provided";
        }

        public override Task Act()
        {

            ProgramSettings.Tetris = new Tetris(path);
            return Task.CompletedTask;
        }
    }
}