public class Board
{
    Random rand;
    Coordinate currentPos;
    TetrisShape currentShape;
    public int boardOffset { get; set; } = 1;
    public char[,] board { get; set; }
    public TetirsColor[,] colorBoard { get; set; }

    public Board()
    {
        board = new char[20, 10];
        InitBoardWithChar(' ');
        colorBoard = new TetirsColor[20, 10];
        rand = new Random(Guid.NewGuid().GetHashCode());
        currentPos = new Coordinate(TetrisUtils.CyclicY(boardOffset - 1), 4);
        currentShape = PickRandomBlock();
        UpdateShape();
    }

    private void InitBoardWithChar(char character)
    {
        for (int y = 0; y < TetrisConstants.BoardHeight; y++)
        {
            for (int x = 0; x < TetrisConstants.BoardWidth; x++)
            {
                board[y, x] = ' ';
            }
        }
    }

    private TetrisShape PickRandomBlock()
    {
        return (TetrisShape)TetrisConstants.TetrisShapeList[rand.Next(TetrisConstants.TetrisShapeList.Length)].Clone();
    }

    public bool PickNewBlock()
    {
        bool gameStillRunning = true;
        currentPos.Y = TetrisUtils.CyclicY(boardOffset - 1);
        currentPos.X = 4;
        currentShape = PickRandomBlock();
        if (CheckLoseCondition(currentPos, currentShape))
        {
            gameStillRunning = false;
        }
        UpdateShape(currentPos, currentShape);
        return gameStillRunning;
    }

    public void UpdateShape(Coordinate position, TetrisShape tetrisShape)
    {
        Coordinate pos;
        foreach (var coord in tetrisShape.Shape)
        {
            pos = coord + position;
            board[TetrisUtils.CyclicY(pos.Y), pos.X] = '\u25A0';
            colorBoard[TetrisUtils.CyclicY(pos.Y), pos.X] = tetrisShape.Color;
        }
    }
    public void UpdateShape()
    {
        UpdateShape(currentPos, currentShape);
    }

    public void UndoShape(Coordinate position, TetrisShape tetrisShape)
    {
        Coordinate pos;
        foreach (var coord in tetrisShape.Shape)
        {
            pos = coord + position;
            board[TetrisUtils.CyclicY(pos.Y), pos.X] = ' ';
            colorBoard[TetrisUtils.CyclicY(pos.Y), pos.X] = TetirsColor.Gray;
        }
    }

    public void UndoShape()
    {
        UndoShape(currentPos, currentShape);
    }

    public bool IsValidMove(Coordinate position, TetrisShape tetrisShape)
    {
        foreach (var coord in tetrisShape.Shape)
        {
            if (position.X + coord.X < 0 ||
                position.X + coord.X >= TetrisConstants.BoardWidth ||
                TetrisUtils.CartesanYToOffsetY(position.Y + coord.Y, boardOffset) >= TetrisConstants.BoardHeight - 1 ||
                board[TetrisUtils.CyclicY(position.Y + coord.Y), position.X + coord.X] != ' ')
            {
                return false;
            }
        }
        return true;
    }

    public bool IsValidMove()
    {
        return IsValidMove(currentPos, currentShape);
    }

    public bool GravityUpdate(Coordinate position, TetrisShape tetrisShape)
    {
        currentPos.Y = TetrisUtils.CyclicY(currentPos.Y + 1);
        bool isValidMove = IsValidMove(position, tetrisShape);
        if (!isValidMove)
        {
            currentPos.Y = TetrisUtils.CyclicY(currentPos.Y - 1);
        }
        UpdateShape(position, tetrisShape);
        return isValidMove;
    }

    public bool GravityUpdate()
    {
        return GravityUpdate(currentPos, currentShape);
    }

    public int EliminateRows()
    {
        int i = 0;
        int rowCount = 0;
        bool bottomRows = true;
        bool rowComplete;
        for (int y = TetrisUtils.CyclicY(boardOffset - 2); i < TetrisConstants.BoardHeight; i++)
        {
            Console.WriteLine($"checking y: {y}");
            rowComplete = true;
            for (int x = 0; x < TetrisConstants.BoardWidth; x++)
            {
                if (board[y, x] == ' ')
                {
                    rowComplete = false;
                    break;
                }
            }
            if (rowComplete)
            {
                rowCount++;
                for (int x = 0; x < TetrisConstants.BoardWidth; x++)
                {
                    board[y, x] = ' ';
                }
                if (bottomRows)
                {
                    boardOffset--;
                }
            }
            else
            {
                if (bottomRows)
                {
                    bottomRows = false;
                }
            }
            y = TetrisUtils.CyclicY(y - 1);
        }
        boardOffset = TetrisUtils.CyclicY(boardOffset);
        // System.Threading.Thread.Sleep(2000);
        return rowCount;
    }

    public bool CheckLoseCondition(Coordinate position, TetrisShape tetrisShape)
    {
        Coordinate pos;
        foreach (var coord in tetrisShape.Shape)
        {
            pos = position + coord;
            if (board[TetrisUtils.CyclicY(pos.Y), pos.X] != ' ')
            {
                return true;
            }
        }
        return false;
    }

    public void MoveLeft()
    {
        currentPos.X -= 1;
        if (!IsValidMove())
        {
            currentPos.X += 1;
        }
    }

    public void MoveRight()
    {
        currentPos.X += 1;
        if (!IsValidMove(currentPos, currentShape))
        {
            currentPos.X -= 1;
        }
    }

    public void MoveUp()
    {
        currentShape.RotateRight();
        if (!IsValidMove(currentPos, currentShape))
        {
            currentShape.UndoRotate();
        }
    }

    public int MoveDown()
    {
        while (true)
        {
            currentPos.Y = TetrisUtils.CyclicY(currentPos.Y + 1);
            if (!IsValidMove(currentPos, currentShape))
            {
                currentPos.Y = TetrisUtils.CyclicY(currentPos.Y - 1);
                break;
            }
        }
        UpdateShape(currentPos, currentShape);
        PickNewBlock();
        return EliminateRows();
    }
}
