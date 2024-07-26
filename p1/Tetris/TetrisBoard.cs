namespace TetrisGame;
using TetrisResources;

public class TetrisBoard
{
    int randSeed;
    Random rand;
    int randCount;
    public Coordinate CurrentPos { get; set; }
    public Shape CurrentShape { get; set; }
    public Shape NextShape { get; set; }
    public LinkedList Board { get; set; }
    public Color[,] ColorBoard { get; set; }


    public TetrisBoard()
    {
        Board = new LinkedList();
        ColorBoard = new Color[20, 10];
        rand = new Random(randSeed);
        CurrentPos = new Coordinate(0, 4);
        CurrentShape = PickRandomBlock();
        NextShape = PickRandomBlock();
    }

    public TetrisBoard(GameState gameState)
    {
        Board = GameState.ListBoardToLinkedList(gameState.Board);
        ColorBoard = GameState.ListColorBoardToColorBoard(gameState.ColorBoard);
        randSeed = gameState.RandSeed;
        randCount = gameState.RandCount;
        rand = new Random(randSeed);
        RestoreRandState(randCount);
        NextShape = gameState.NextShape;
        CurrentPos = gameState.CurrentPos;
        CurrentShape = gameState.CurrentShape;
    }

    public void SetSeed(int seed)
    {
        rand = new Random(seed);
        randSeed = seed;
        CurrentShape = PickRandomBlock();
        NextShape = PickRandomBlock();
    }

    private void RestoreRandState(int randCalled)
    {
        for (int i = 0; i < randCalled; i++)
        {
            rand.Next(TetrisConstants.TetrisShapeList.Length);
        }
    }

    private Shape PickRandomBlock()
    {
        randCount++;
        return (Shape)TetrisConstants.TetrisShapeList[rand.Next(TetrisConstants.TetrisShapeList.Length)].Clone();
    }

    public bool PickNewBlock()
    {
        bool gameStillRunning = true;
        CurrentPos.Y = 0;
        CurrentPos.X = 4;
        CurrentShape = NextShape;
        NextShape = PickRandomBlock();
        if (CheckLoseCondition())
        {
            gameStillRunning = false;
        }
        UpdateShape();
        return gameStillRunning;
    }

    public void UpdateShape()
    {
        Coordinate pos;
        foreach (var coord in CurrentShape.Shapes)
        {
            pos = coord + CurrentPos;
            Board.ElementAt(pos.Y)[pos.X] = TetrisConstants.TetrisBlock;
            ColorBoard[pos.Y, pos.X] = CurrentShape.Color;
        }
    }

    public void UndoShape()
    {
        Coordinate pos;
        foreach (var coord in CurrentShape.Shapes)
        {
            pos = coord + CurrentPos;
            Board.ElementAt(pos.Y)[pos.X] = TetrisConstants.Empty;
            ColorBoard[pos.Y, pos.X] = Color.DefaultColor;
        }
    }

    public bool IsValidMove()
    {
        foreach (var coord in CurrentShape.Shapes)
        {
            if (CurrentPos.X + coord.X < 0 ||
                CurrentPos.X + coord.X >= TetrisConstants.BoardWidth ||
                CurrentPos.Y + coord.Y >= TetrisConstants.BoardHeight ||
                Board.ElementAt(CurrentPos.Y + coord.Y)[CurrentPos.X + coord.X] != TetrisConstants.Empty)
            {
                return false;
            }
        }
        return true;
    }


    public bool GravityUpdate()
    {
        this.CurrentPos.Y++;
        bool isValidMove = IsValidMove();
        if (!isValidMove)
        {
            this.CurrentPos.Y--;
        }
        UpdateShape();
        return isValidMove;
    }


    public int EliminateRows()
    {
        int rowCount = 0;
        bool rowComplete;

        LinkedListNode? node = Board.First;
        LinkedListNode? last = null;
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
                    node.Next = Board.First;
                    Board.First = node;
                }


            }

            last = node;
            node = last.Next;
        }

        return rowCount;
    }

    public bool CheckLoseCondition()
    {
        Coordinate pos;
        foreach (var coord in CurrentShape.Shapes)
        {
            pos = CurrentPos + coord;
            if (Board.ElementAt(pos.Y)[pos.X] != TetrisConstants.Empty)
            {
                return true;
            }
        }
        return false;
    }

    public void MoveLeft()
    {
        CurrentPos.X -= 1;
        if (!IsValidMove())
        {
            CurrentPos.X += 1;
        }
        UpdateShape();
    }

    public void MoveRight()
    {
        CurrentPos.X += 1;
        if (!IsValidMove())
        {
            CurrentPos.X -= 1;
        }
        UpdateShape();
    }

    public void MoveUp()
    {
        CurrentShape.RotateRight();
        if (!IsValidMove())
        {
            CurrentShape.UndoRotate();
        }
        UpdateShape();
    }

    // public int MoveDown()
    // {
    //     while (true)
    //     {
    //         currentPos.Y++;
    //         if (!IsValidMove(currentPos, currentShape))
    //         {
    //             currentPos.Y--;
    //             break;
    //         }
    //     }
    //     UpdateShape(currentPos, currentShape);
    //     int rowsEliminated = EliminateRows();
    //     PickNewBlock();
    //     return rowsEliminated;
    // }
}
