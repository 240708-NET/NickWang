namespace TetrisRepo;

public interface IGameRepository
{
    public List<Game> All();
    public Game? FindById(int Id);
    public List<string> GetHighScores(int amount);
    public Game Write(Game game);
}