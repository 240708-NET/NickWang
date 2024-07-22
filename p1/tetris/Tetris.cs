using System.Diagnostics;

public class Tetris
{

    long GravityUpdateBlockTimeMilliseconds = 1000;
    int bottomOfBoard = TetrisConstants.BoardHeight;
    char[,] board = new char[20, 10];
    TetirsColor[,] colorBoard = new TetirsColor[20, 10];
    Random rand;



    bool gameRunning = true;
    ConsoleKeyInfo keyinfo;
    Stopwatch? gravityWatch;

    Coordinate currentPos;
    TetrisShape currentShape;

    public Tetris()
    {
        for (int y = 0; y < TetrisConstants.BoardHeight; y++)
        {
            for (int x = 0; x < TetrisConstants.BoardWidth; x++)
            {
                board[y, x] = ' ';
            }
        }
        rand = new Random(Guid.NewGuid().GetHashCode());
        currentPos = new Coordinate(6, 4);
        currentShape = PickRandomBlock();
        // currentShape = (TetrisShape)TetrisConstants.LBlock.Clone();

    }

    private TetrisShape PickRandomBlock()
    {
        return (TetrisShape)TetrisConstants.TetrisShapeList[rand.Next(TetrisConstants.TetrisShapeList.Length)].Clone();
    }

    public void Run()
    {
        // Stopwatch inputWatch = Stopwatch.StartNew();
        gravityWatch = Stopwatch.StartNew();
        UpdateShape(currentPos, currentShape);
        Display();
        while (gameRunning)
        {
            HandleKeyPress();
            HandleGravityUpdate(gravityWatch);
        }
    }

    private void HandleGravityUpdate(Stopwatch watch)
    {
        if (watch.ElapsedMilliseconds >= GravityUpdateBlockTimeMilliseconds)
        {
            UndoShape(currentPos, currentShape);
            if (!GravityUpdate(currentPos, currentShape))
            {
                PickNewBlock();
            }
            Display();
            watch.Restart();
        }
    }

    private bool CheckLoseCondition(Coordinate position, TetrisShape tetrisShape)
    {
        Coordinate pos;
        foreach (var coord in tetrisShape.Shape)
        {
            pos = position + coord;
            if (board[pos.Y, pos.X] != ' ')
            {
                return true;
            }
        }
        return false;
    }


    private void HandleKeyPress()
    {
        if (Console.KeyAvailable)
        {
            keyinfo = Console.ReadKey(true);
            UndoShape(currentPos, currentShape);
            switch (keyinfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    currentPos.X -= 1;
                    if (!IsValidMove(currentPos, currentShape))
                    {
                        currentPos.X += 1;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    currentPos.X += 1;
                    if (!IsValidMove(currentPos, currentShape))
                    {
                        currentPos.X -= 1;
                    }
                    break;
                case ConsoleKey.UpArrow:
                    currentShape.RotateRight();
                    break;
                case ConsoleKey.DownArrow:
                    while (true)
                    {
                        currentPos.Y += 1;
                        if (!IsValidMove(currentPos, currentShape))
                        {
                            currentPos.Y -= 1;
                            break;
                        }
                    }
                    UpdateShape(currentPos, currentShape);
                    PickNewBlock();
                    break;
                default:
                    break;
            }
            UpdateShape(currentPos, currentShape);
            Display();
        }
    }

    void PickNewBlock()
    {
        currentPos.Y = 0;
        currentPos.X = 4;
        currentShape = PickRandomBlock();
        if (CheckLoseCondition(currentPos, currentShape))
        {
            gameRunning = false;
        }
        UpdateShape(currentPos, currentShape);
    }

    bool GravityUpdate(Coordinate position, TetrisShape tetrisShape)
    {
        position.Y += 1;
        bool isValidMove = IsValidMove(position, tetrisShape);
        if (!isValidMove)
        {
            currentPos.Y -= 1;
        }
        UpdateShape(position, tetrisShape);
        return isValidMove;
    }

    void UpdateShape(Coordinate position, TetrisShape tetrisShape)
    {
        Coordinate pos;
        foreach (var coord in tetrisShape.Shape)
        {
            pos = coord + position;
            board[pos.Y, pos.X] = '\u25A0';
            colorBoard[pos.Y, pos.X] = tetrisShape.Color;
        }
    }

    void UndoShape(Coordinate position, TetrisShape tetrisShape)
    {
        Coordinate pos;
        foreach (var coord in tetrisShape.Shape)
        {
            pos = coord + position;
            board[pos.Y, pos.X] = ' ';
            colorBoard[pos.Y, pos.X] = TetirsColor.Gray;
        }
    }

    bool IsValidMove(Coordinate position, TetrisShape tetrisShape)
    {
        foreach (var coord in tetrisShape.Shape)
        {
            if (position.X + coord.X < 0 ||
                position.X + coord.X >= TetrisConstants.BoardWidth ||
                position.Y + coord.Y >= TetrisConstants.BoardHeight ||
                board[position.Y + coord.Y, position.X + coord.X] != ' ')
            {
                return false;
            }
        }
        return true;
    }

    public void Display()
    {
        Console.Clear();
        for (int y = 0; y < TetrisConstants.BoardHeight; y++)
        {
            for (int x = 0; x < TetrisConstants.BoardWidth; x++)
            {
                SelectColor(colorBoard[y, x]);
                Console.Write(board[y, x]);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write('|');
            Console.WriteLine();
        }
    }

    void SelectColor(TetirsColor color)
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
