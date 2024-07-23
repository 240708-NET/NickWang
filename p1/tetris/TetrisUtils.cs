
public class TetrisUtils
{

    public static void SelectColor(TetirsColor color)
    {
        switch (color)
        {
            case TetirsColor.Cyan:
                Console.ForegroundColor = ConsoleColor.Cyan;
                break;
            case TetirsColor.Blue:
                Console.ForegroundColor = ConsoleColor.Blue;
                break;
            case TetirsColor.White:
                Console.ForegroundColor = ConsoleColor.White;
                break;
            case TetirsColor.Yellow:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case TetirsColor.Green:
                Console.ForegroundColor = ConsoleColor.Green;
                break;
            case TetirsColor.Magenta:
                Console.ForegroundColor = ConsoleColor.Magenta;
                break;
            case TetirsColor.Red:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Gray;
                break;
        }
    }
}

