using TetrisGame;

namespace CLIArgs
{

    class NewUserArg : CommandLineArg
    {

        string playerName = "";

        public NewUserArg(string flag) : base(flag) { }
        public override void ConsumeParameters(string[] args, ref int index)
        {
            try
            {
                playerName = args[++index];
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
            return $"   -{flag} <name> the display name of the new player to be created. if a name is provided a new player will be created regardless if and id is also provided";
        }


        public override Task Act()
        {

            ProgramSettings.PlayerName = playerName;
            return Task.CompletedTask;
        }
    }
}