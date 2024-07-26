namespace TetrisResources;

public enum Color
{
    IBlockColor,
    LBlockColor,
    OBlockColor,
    JBlockColor,
    SBlockColor,
    TBlockColor,
    ZBlockColor,
    DefaultColor
}


public class ColorHelper
{
    public static void SelectColor(Color color)
    {
        switch (color)
        {
            case Color.IBlockColor:
                Console.ForegroundColor = ConsoleColor.Cyan;
                break;
            case Color.LBlockColor:
                Console.ForegroundColor = ConsoleColor.Blue;
                break;
            case Color.OBlockColor:
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                break;
            case Color.JBlockColor:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case Color.SBlockColor:
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case Color.TBlockColor:
                Console.ForegroundColor = ConsoleColor.Magenta;
                break;
            case Color.ZBlockColor:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Gray;
                break;
        }
    }

}

