
public class TetrisConstants
{
    public static readonly int BoardWidth = 10;
    public static readonly int BoardHeight = 20;
    public static readonly TetrisShape IBlock;
    public static readonly TetrisShape LBlock;
    public static readonly TetrisShape OBlock;
    public static readonly TetrisShape JBlock;
    public static readonly TetrisShape SBlock;
    public static readonly TetrisShape TBlock;
    public static readonly TetrisShape ZBlock;
    public static readonly TetrisShape[] TetrisShapeList;

    static TetrisConstants()
    {
        IBlock = new TetrisShape
        {
            Dimension = new Coordinate(4, 1),
            Shape = [new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(2, 0), new Coordinate(3, 0)],
            Color = TetirsColor.Cyan
        };
        JBlock = new TetrisShape
        {
            Dimension = new Coordinate(3, 2),
            Shape = [new Coordinate(0, 1), new Coordinate(1, 1), new Coordinate(2, 0), new Coordinate(2, 1)],
            Color = TetirsColor.Green
        };
        LBlock = new TetrisShape
        {
            Dimension = new Coordinate(3, 2),
            Shape = [new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(2, 0), new Coordinate(2, 1)],
            Color = TetirsColor.Blue
        };
        OBlock = new TetrisShape
        {
            Dimension = new Coordinate(2, 2),
            Shape = [new Coordinate(0, 0), new Coordinate(0, 1), new Coordinate(1, 0), new Coordinate(1, 1)],
            Color = TetirsColor.Yellow
        };
        SBlock = new TetrisShape
        {
            Dimension = new Coordinate(2, 3),
            Shape = [new Coordinate(0, 1), new Coordinate(0, 2), new Coordinate(1, 0), new Coordinate(1, 1)],
            Color = TetirsColor.Magenta
        };
        TBlock = new TetrisShape
        {
            Dimension = new Coordinate(2, 3),
            Shape = [new Coordinate(0, 1), new Coordinate(1, 0), new Coordinate(1, 1), new Coordinate(1, 2)],
            Color = TetirsColor.Red
        };
        ZBlock = new TetrisShape
        {
            Dimension = new Coordinate(2, 3),
            Shape = [new Coordinate(0, 0), new Coordinate(0, 1), new Coordinate(1, 1), new Coordinate(1, 2)],
            Color = TetirsColor.White
        };

        TetrisShapeList = [IBlock, JBlock, LBlock, OBlock, SBlock, TBlock, ZBlock];
    }

}


