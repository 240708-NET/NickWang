namespace TetrisRepo;

public interface IPlayerRepository
{
    public List<Player> All();
    public Player? FindById(int id);
    public Player? IncreaseGameCount(int id);
    public Player Write(Player player);
}
