using System.Diagnostics;

public partial class Tetris
{

    long GravityUpdateBlockTimeMilliseconds = 1000;

    Board board;


    bool gameRunning = true;
    ConsoleKeyInfo keyinfo;
    Stopwatch? gravityWatch;


    public Tetris()
    {
        board = new Board();
    }

    public void Run()
    {
        // Stopwatch inputWatch = Stopwatch.StartNew();
        gravityWatch = Stopwatch.StartNew();
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
            board.UndoShape();
            if (!board.GravityUpdate())
            {
                gameRunning = board.PickNewBlock();
                board.EliminateRows();
            }
            Display();
            watch.Restart();
        }
    }

    private void HandleKeyPress()
    {
        if (Console.KeyAvailable)
        {
            keyinfo = Console.ReadKey(true);
            board.UndoShape();
            switch (keyinfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    board.MoveLeft();
                    break;
                case ConsoleKey.RightArrow:
                    board.MoveRight();
                    break;
                case ConsoleKey.UpArrow:
                    board.MoveUp();
                    break;
                case ConsoleKey.DownArrow:
                    board.MoveDown();
                    break;
                default:
                    break;
            }
            board.UpdateShape();
            Display();
        }
    }

    public void Display()
    {
        Console.Clear();
        int i = 0;
        for (int y = TetrisUtils.CyclicY(board.boardOffset - 1); i < TetrisConstants.BoardHeight; i++)
        {
            for (int x = 0; x < TetrisConstants.BoardWidth; x++)
            {
                TetrisUtils.SelectColor(board.colorBoard[y, x]);
                Console.Write(board.board[y, x]);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write('|');
            Console.Write(y);
            Console.WriteLine();
            y = TetrisUtils.CyclicY(y + 1);
        }
    }
}
