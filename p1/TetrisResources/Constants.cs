namespace TetrisResources;

public class TetrisConstants
{
    public static readonly int BoardWidth = 10;
    public static readonly int BoardHeight = 20;
    public static readonly char TetrisBlock = '\u25A0';
    public static readonly char Empty = ' ';
    public static readonly long BaseGravityUpdateBlockTimeMilliseconds = 100;
    public static readonly Shape IBlock;
    public static readonly Shape LBlock;
    public static readonly Shape OBlock;
    public static readonly Shape JBlock;
    public static readonly Shape SBlock;
    public static readonly Shape TBlock;
    public static readonly Shape ZBlock;
    public static readonly Shape[] TetrisShapeList;

    static TetrisConstants()
    {
        IBlock = new Shape
        {
            Dimension = new Coordinate(4, 1),
            Shapes = [new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(2, 0), new Coordinate(3, 0)],
            Color = Color.IBlockColor
        };
        JBlock = new Shape
        {
            Dimension = new Coordinate(3, 2),
            Shapes = [new Coordinate(0, 1), new Coordinate(1, 1), new Coordinate(2, 0), new Coordinate(2, 1)],
            Color = Color.JBlockColor
        };
        LBlock = new Shape
        {
            Dimension = new Coordinate(3, 2),
            Shapes = [new Coordinate(0, 0), new Coordinate(1, 0), new Coordinate(2, 0), new Coordinate(2, 1)],
            Color = Color.LBlockColor
        };
        OBlock = new Shape
        {
            Dimension = new Coordinate(2, 2),
            Shapes = [new Coordinate(0, 0), new Coordinate(0, 1), new Coordinate(1, 0), new Coordinate(1, 1)],
            Color = Color.OBlockColor
        };
        SBlock = new Shape
        {
            Dimension = new Coordinate(2, 3),
            Shapes = [new Coordinate(0, 1), new Coordinate(0, 2), new Coordinate(1, 0), new Coordinate(1, 1)],
            Color = Color.SBlockColor
        };
        TBlock = new Shape
        {
            Dimension = new Coordinate(2, 3),
            Shapes = [new Coordinate(0, 1), new Coordinate(1, 0), new Coordinate(1, 1), new Coordinate(1, 2)],
            Color = Color.TBlockColor
        };
        ZBlock = new Shape
        {
            Dimension = new Coordinate(2, 3),
            Shapes = [new Coordinate(0, 0), new Coordinate(0, 1), new Coordinate(1, 1), new Coordinate(1, 2)],
            Color = Color.ZBlockColor
        };

        TetrisShapeList = [IBlock, JBlock, LBlock, OBlock, SBlock, TBlock, ZBlock];
    }

}


