public class Board
{
    Random rand;
    Coordinate currentPos;
    TetrisShape currentShape;
    public TetrisShape nextShape { get; set; }
    public TetrisLinkedList board { get; set; }
    public TetirsColor[,] colorBoard { get; set; }


    public Board()
    {
        board = new TetrisLinkedList();
        colorBoard = new TetirsColor[20, 10];
        rand = new Random(Guid.NewGuid().GetHashCode());
        currentPos = new Coordinate(0, 4);
        currentShape = PickRandomBlock();
        nextShape = PickRandomBlock();
        UpdateShape();
    }

    private TetrisShape PickRandomBlock()
    {
        return (TetrisShape)TetrisConstants.TetrisShapeList[rand.Next(TetrisConstants.TetrisShapeList.Length)].Clone();
    }

    public bool PickNewBlock()
    {
        bool gameStillRunning = true;
        currentPos.Y = 0;
        currentPos.X = 4;
        currentShape = nextShape;
        nextShape = PickRandomBlock();
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
            board.ElementAt(pos.Y)[pos.X] = TetrisConstants.TetrisBlock;
            colorBoard[pos.Y, pos.X] = tetrisShape.Color;
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
            board.ElementAt(pos.Y)[pos.X] = TetrisConstants.Empty;
            colorBoard[pos.Y, pos.X] = TetirsColor.Gray;
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
                position.Y + coord.Y >= TetrisConstants.BoardHeight ||
                board.ElementAt(position.Y + coord.Y)[position.X + coord.X] != TetrisConstants.Empty)
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
        currentPos.Y++;
        bool isValidMove = IsValidMove(position, tetrisShape);
        if (!isValidMove)
        {
            currentPos.Y--;
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
        int rowCount = 0;
        bool rowComplete;

        TetrisLinkedListNode? node = board.First;
        TetrisLinkedListNode? last = null;
        TetrisLinkedListNode? temp;
        while (node != null)
        {

            rowComplete = true;
            for (int x = 0; x < TetrisConstants.BoardWidth; x++)
            {
                if (node.Row[x] == TetrisConstants.Empty)
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
                    node.Row[x] = TetrisConstants.Empty;
                }

                if (last != null)
                {
                    last.Next = node.Next;
                    node.Next = board.First;
                    board.First = node;
                }


            }

            temp = last;
            last = node;
            node = last.Next;
        }

        return rowCount;
    }

    public bool CheckLoseCondition(Coordinate position, TetrisShape tetrisShape)
    {
        Coordinate pos;
        foreach (var coord in tetrisShape.Shape)
        {
            pos = position + coord;
            if (board.ElementAt(pos.Y)[pos.X] != TetrisConstants.Empty)
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
            currentPos.Y++;
            if (!IsValidMove(currentPos, currentShape))
            {
                currentPos.Y--;
                break;
            }
        }
        UpdateShape(currentPos, currentShape);
        int rowsEliminated = EliminateRows();
        PickNewBlock();
        return rowsEliminated;
    }
}
