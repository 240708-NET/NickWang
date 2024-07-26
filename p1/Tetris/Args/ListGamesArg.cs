using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using TetrisGame;

namespace CLIArgs
{
    class ListGamesArg : CommandLineArg
    {
        public ListGamesArg(string flag) : base(flag) { }

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
            return $"   -{flag} list all existing games";
        }


        public override async Task Act()
        {
            Console.WriteLine(String.Format("{0,-6}|{1, -6}|{2, -13}|{3,-6}", "Id", "Score", "Lines Cleared", "PlayerId", "Time"));
            HttpClient client = new HttpClient();
            var res = await client.GetAsync($"{ProgramSettings.DatabaseURL}/Game");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var games = JsonSerializer.Deserialize<List<Game>>(res.Content.ReadAsStream(), options);
            foreach (var game in games)
            {
                Console.WriteLine(game.ToString());
            }
            Environment.Exit(0);
        }
    }
}