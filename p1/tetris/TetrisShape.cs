
public class TetrisShape : ICloneable
{
    public Coordinate Dimension { get; set; }
    public Coordinate[] Shape { get; set; }
    public TetirsColor Color { get; set; }

    int orientation;

    public TetrisShape()
    {
        Dimension = new Coordinate(0, 0);
        Shape = Array.Empty<Coordinate>();
        Color = TetirsColor.Red;
    }

    public TetrisShape(Coordinate dimension, Coordinate[] shape, TetirsColor color)
    {
        Dimension = dimension;
        Shape = shape;
        Color = color;
    }

    public void RotateRight()
    {
        orientation = (orientation + 1) % 4;
        Dimension.RotateDimensionRight();
        foreach (Coordinate c in Shape)
        {
            c.RotateRight(Dimension);
        }
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}
