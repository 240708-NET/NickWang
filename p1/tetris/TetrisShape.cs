
public class TetrisShape : ICloneable
{
    public Coordinate Dimension { get; set; }
    public Coordinate[] Shape { get; set; }
    public TetirsColor Color { get; set; }

    public TetrisShape()
    {
        Dimension = new Coordinate(0, 0);
        Shape = Array.Empty<Coordinate>();
        Color = TetirsColor.Gray;
    }

    public TetrisShape(Coordinate dimension, Coordinate[] shape, TetirsColor color)
    {
        Dimension = dimension;
        Shape = shape;
        Color = color;
    }

    public void RotateRight()
    {
        Dimension.RotateDimensionRight();
        foreach (Coordinate c in Shape)
        {
            c.RotateRight(Dimension);
        }
    }

    public void UndoRotate()
    {
        foreach (Coordinate c in Shape)
        {
            c.UndoRotate(Dimension);
        }
        Dimension.RotateDimensionRight();
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}
