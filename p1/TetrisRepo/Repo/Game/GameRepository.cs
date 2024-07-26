namespace TetrisRepo;

public class GameRepository : IGameRepository
{
    private readonly DataContext _dataContext;
    public GameRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public List<Game> All()
    {
        return _dataContext.Games.ToList();
    }

    public Game FindById(int Id)
    {
        Game? game = _dataContext.Games.Find(Id);
        if (game == null)
        {
            throw new Exception("");
        }
        return game;
    }

    public List<string> GetHighScores(int amount)
    {
        var games = _dataContext.Games;
        var players = _dataContext.Players;

        var res = (from game in games
                   join player in players
                   on game.PlayerId equals player.Id into gj
                   from subgroup in gj.DefaultIfEmpty()
                   orderby game.Score descending, game.Time descending
                   select new
                   {
                       game.Score,
                       game.LinesCleared,
                       game.Time,
                       PlayerName = subgroup.Name
                   }).Take(amount);

        ;
        return res.Select(r => String.Format("{0,-6}|{1, -6}|{2, -15}|{3,-6}", r.Score, r.LinesCleared, r.PlayerName == null ? "Anynomous" : r.PlayerName, r.Time.ToLongTimeString())).ToList();
    }

    public Game Write(Game game)
    {
        _dataContext.Games.Add(game);
        _dataContext.SaveChanges();
        return game;
    }
}