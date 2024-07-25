public interface IPlayerRepo
{
    public Player? FindById(int id);
    public void IncreaseGameCount(int id);
    public int Write(Player player);
}
