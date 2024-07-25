using System.Text.Json;

public class GameStateRepoImpl : IGameStateRepo
{
    public TetrisGameState FindByPath(string path)
    {
        string json = "";
        try
        {
            json = File.ReadAllText($"{path}");
        }
        catch (System.IO.DirectoryNotFoundException)
        {
            Console.WriteLine("File Cannot be Found!!");
            Environment.Exit(1);
        }
        JsonSerializerOptions options = new JsonSerializerOptions();
        TetrisGameState? state = JsonSerializer.Deserialize<TetrisGameState>(json);
        if (state == null)
        {
            throw new ArgumentException("");
        }
        return state;

    }

    public int Write(TetrisGameState data)
    {
        JsonSerializerOptions options = new JsonSerializerOptions();
        string json = JsonSerializer.Serialize<TetrisGameState>(data, options);
        File.WriteAllText($"saves/{DateTime.Now.ToFileTime()}.save", json);

        return 1;
    }
}