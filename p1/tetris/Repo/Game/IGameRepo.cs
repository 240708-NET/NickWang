public interface IGameRepo
{
    public Game FindById(int Id);
    public List<string> GetHighScores();
    public void Write(Game game);
}