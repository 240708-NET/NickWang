public class PlayerRepoImpl : IPlayerRepo
{
    public Player? FindById(int id)
    {
        return ProgramSettings.DataContext.Players.Find(id);
    }

    public void IncreaseGameCount(int id)
    {
        Player? player = ProgramSettings.DataContext.Players.Find(id);
        if (player != null)
        {
            player.GamesPlayed++;
            ProgramSettings.DataContext.SaveChanges();
        }
    }

    public int Write(Player player)
    {
        ProgramSettings.DataContext.Players.Add(player);
        ProgramSettings.DataContext.SaveChanges();
        return player.Id;
    }
}