using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Game
{

    [Key]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Id { get; set; }
    public int Score { get; set; }
    public int LinesCleared { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [ForeignKey("Player")]
    public int PlayerId { get; set; }
    public DateTime Time { get; set; }

    public override string ToString()
    {
        return string.Format("{0,-6}|{1, -6}|{2, -13}|{3,-6}", Id, Score, LinesCleared, PlayerId, Time);
    }

}