using System.ComponentModel.DataAnnotations;

public class Player
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public int GamesPlayed { get; set; }

    public Player(string name)
    {
        Name = name;
    }

}