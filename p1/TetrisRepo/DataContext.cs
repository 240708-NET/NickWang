
using Microsoft.EntityFrameworkCore;

namespace TetrisRepo;
public class DataContext : DbContext
{
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Game> Games => Set<Game>();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string ConnectionString = File.ReadAllText("./connectionstring");
        optionsBuilder.UseSqlServer(ConnectionString);
    }
}