public class GameRepoImpl : IGameRepo
{
    public Game FindById(int Id)
    {
        Game? game = ProgramSettings.DataContext.Games.Find(Id);
        if (game == null)
        {
            throw new Exception("");
        }
        return game;
    }

    public List<string> GetHighScores()
    {
        var games = ProgramSettings.DataContext.Games;
        var players = ProgramSettings.DataContext.Players;
        Player nullPlayer = new Player("");

        var res = (from game in games
                   join player in players
                   on game.Player.Id equals player.Id into gj
                   from subgroup in gj.DefaultIfEmpty()
                   orderby game.Score descending, game.Time descending
                   select new
                   {
                       game.Score,
                       game.LinesCleared,
                       game.Time,
                       PlayerName = subgroup.Name
                   }).Take(20);

        ;
        return res.Select(r => String.Format("{0,-6}|{1, -6}|{2, -10}|{3,-6}", r.Score, r.LinesCleared, r.PlayerName == null ? "Anynomous" : r.PlayerName, r.Time.ToLongTimeString())).ToList();
    }

    public void Write(Game game)
    {
        ProgramSettings.DataContext.Games.Add(game);
        ProgramSettings.DataContext.SaveChanges();
    }
}