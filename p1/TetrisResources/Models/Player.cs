using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Player
{
    [Key]
    // [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Id { get; set; }

    // [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }
    public int GamesPlayed { get; set; }

    public Player() { }
    public Player(string name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return String.Format("{0,-6}|{1, -15}|{2, -6}", Id, Name, GamesPlayed);
    }

}