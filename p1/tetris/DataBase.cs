
public class DataBase
{
    IPlayerRepo playerRepo;
    IGameRepo gameRepo;

    public DataBase(IPlayerRepo playerRepo, IGameRepo gameRepo)
    {
        this.playerRepo = playerRepo;
        this.gameRepo = gameRepo;
    }

    public List<string> GetHighScores()
    {
        return gameRepo.GetHighScores();
    }

    public void UploadGame(int score, int linesCleared, Player? player)
    {
        Game game = new Game
        {
            Score = score,
            LinesCleared = linesCleared,
            Player = player,
            Time = DateTime.Now,
        };
        gameRepo.Write(game);



        if (player != null)
        {
            playerRepo.IncreaseGameCount(player.Id);
        }
    }

    public int CreatePlayer(string playerName)
    {
        return playerRepo.Write(new Player(playerName));
    }

    public Player? FindPlayerById(int id)
    {
        return playerRepo.FindById(id);
    }
}