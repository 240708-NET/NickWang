namespace TetrisRepo;

public class PlayerRepository : IPlayerRepository
{
    private readonly DataContext _dataContext;

    public PlayerRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public List<Player> All()
    {
        return _dataContext.Players.ToList();
    }

    public Player? FindById(int id)
    {
        return _dataContext.Players.Find(id);
    }

    public Player? IncreaseGameCount(int id)
    {
        Player? player = _dataContext.Players.Find(id);
        if (player != null)
        {
            player.GamesPlayed++;
            _dataContext.SaveChanges();
        }
        return player;
    }

    public Player Write(Player player)
    {
        _dataContext.Players.Add(player);
        _dataContext.SaveChanges();
        return new Player
        {
            Id = player.Id,
            Name = player.Name,
            GamesPlayed = player.GamesPlayed,
        };
    }
}