
public class Shape : ICloneable
{
    public Coordinate Dimension { get; set; }
    public Coordinate[] Shapes { get; set; }
    public Color Color { get; set; }

    public Shape()
    {
        Dimension = new Coordinate(0, 0);
        Shapes = Array.Empty<Coordinate>();
        Color = Color.DefaultColor;
    }

    public Shape(Coordinate dimension, Coordinate[] shape, Color color)
    {
        Dimension = dimension;
        Shapes = shape;
        Color = color;
    }

    public void RotateRight()
    {
        Dimension.RotateDimensionRight();
        foreach (Coordinate c in Shapes)
        {
            c.RotateRight(Dimension);
        }
    }

    public void UndoRotate()
    {
        foreach (Coordinate c in Shapes)
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
