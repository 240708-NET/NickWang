using TetrisGame;

namespace CLIArgs
{
    class LoginArg : CommandLineArg
    {

        string playerId = "";

        public LoginArg(string flag) : base(flag) { }
        public override void ConsumeParameters(string[] args, ref int index)
        {
            try
            {
                playerId = args[++index];
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
            return $"   -{flag} <id> the playerId of the player to login. if an invalid id is provided it will be ignored. this arg should not be used with --name";
        }


        public override Task Act()
        {

            ProgramSettings.PlayerId = Int32.Parse(playerId);
            return Task.CompletedTask;
        }
    }
}