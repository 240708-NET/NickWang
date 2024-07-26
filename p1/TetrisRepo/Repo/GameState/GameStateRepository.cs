using System.Text.Json;
using TetrisResources;

namespace TetrisRepo;

public class GameStateRepository : IGameStateRepository
{
    public GameState FindByPath(string path)
    {
        string json = "";
        try
        {
            json = File.ReadAllText($"saves/{path}");
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("File Cannot be Found!!");
            Environment.Exit(1);
        }
        JsonSerializerOptions options = new JsonSerializerOptions();
        GameState? state = JsonSerializer.Deserialize<GameState>(json);
        if (state == null)
        {
            throw new ArgumentException("");
        }
        return state;

    }

    public int Write(GameState data)
    {
        JsonSerializerOptions options = new JsonSerializerOptions();
        string json = JsonSerializer.Serialize<GameState>(data, options);

        if (!Directory.Exists("saves"))
        {
            Directory.CreateDirectory("saves");
        }
        File.WriteAllText($"saves/{DateTime.Now.ToFileTime()}.save", json);


        return 1;
    }
}