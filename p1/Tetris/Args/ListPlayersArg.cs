using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using TetrisGame;

namespace CLIArgs
{
    class ListPlayersArg : CommandLineArg
    {
        public ListPlayersArg(string flag) : base(flag) { }

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
            return $"   -{flag} list all existing players";
        }


        public override async Task Act()
        {
            Console.WriteLine(String.Format("{0,-6}|{1, -15}|{2, -6}", "Id", "Name", "Games Played"));
            HttpClient client = new HttpClient();
            var res = await client.GetAsync($"{ProgramSettings.DatabaseURL}/Player");
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var players = JsonSerializer.Deserialize<List<Player>>(res.Content.ReadAsStream(), options);
            foreach (var player in players)
            {
                Console.WriteLine(player.ToString());
            }
            Environment.Exit(0);
        }
    }
}