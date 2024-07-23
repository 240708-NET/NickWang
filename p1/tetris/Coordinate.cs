

public class Coordinate : ICloneable
{
    public int Y { get; set; }
    public int X { get; set; }

    static int[] sin = [0, 1, 0, -1];
    static int[] cos = [1, 0, -1, 0];

    public Coordinate(int y, int x)
    {
        Y = y;
        X = x;
    }

    public Coordinate(Coordinate coordinate)
    {
        Y = coordinate.Y;
        X = coordinate.X;
    }

    public static Coordinate operator +(Coordinate a, Coordinate b)
        => new Coordinate(a.Y + b.Y, a.X + b.X);

    public static Coordinate operator -(Coordinate a, Coordinate b)
        => new Coordinate(a.Y - b.Y, a.X - b.X);



    public void RotateRight(Coordinate dimension)
    {
        int tempX = X;
        X = -Y;
        Y = tempX;
        X += dimension.X - 1;
    }

    public void UndoRotate(Coordinate dimension)
    {
        X -= dimension.X - 1;
        int tempX = X;
        X = Y;
        Y = -tempX;
    }

    public void RotateDimensionRight()
    {
        int tempX = X;
        X = Y;
        Y = tempX;
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}