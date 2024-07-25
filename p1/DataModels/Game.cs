using System.ComponentModel.DataAnnotations;

public class Game
{

    [Key]
    public int Id { get; set; }
    public int Score { get; set; }
    public int LinesCleared { get; set; }
    public Player? Player { get; set; }
    public DateTime Time { get; set; }

    // public Game(int score, int linesCleared, Player? player)
    // {
    //     this.score = score;
    //     this.linesCleared = linesCleared;
    //     this.player = player;
    //     time = DateTime.Now;
    // }

}