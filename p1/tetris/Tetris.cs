using System.Diagnostics;

public partial class Tetris
{

    long GravityUpdateBlockTimeMilliseconds = 1000;
    long InputDelayTimeMilliseconds = 10;

    Board board;

    int totalLineCleared;
    int score;


    bool gameRunning = true;
    ConsoleKeyInfo keyinfo;
    Stopwatch gravityWatch;
    Stopwatch inputWatch;


    public Tetris()
    {
        board = new Board();
        inputWatch = Stopwatch.StartNew();
        gravityWatch = Stopwatch.StartNew();
    }

    public void Run()
    {
        Console.CursorVisible = false;
        Display();
        while (gameRunning)
        {
            HandleKeyPress();
            HandleGravityUpdate(false);
        }
    }

    private void HandleGravityUpdate(bool byPassTimer)
    {
        if (byPassTimer ||
            gravityWatch.ElapsedMilliseconds >= GravityUpdateBlockTimeMilliseconds)
        {
            board.UndoShape();
            if (!board.GravityUpdate())
            {
                UpdateScore(board.EliminateRows());
                gameRunning = board.PickNewBlock();
            }
            Display();
            gravityWatch.Restart();
        }
    }

    private void HandleKeyPress()
    {
        if (Console.KeyAvailable &&
            inputWatch.ElapsedMilliseconds >= InputDelayTimeMilliseconds)
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
                    // UpdateScore(board.MoveDown());
                    HandleGravityUpdate(true);
                    break;
                default:
                    break;
            }
            board.UpdateShape();
            Display();
            inputWatch.Restart();
        }
    }

    private void UpdateScore(int linesCleared)
    {
        score += linesCleared * linesCleared * 100;
        totalLineCleared += linesCleared;
    }

    public void Display()
    {
        Console.Clear();
        int y = 0;

        TetrisLinkedListNode? node = board.board.First;
        while (node != null)
        {
            for (int x = 0; x < TetrisConstants.BoardWidth; x++)
            {
                TetrisUtils.SelectColor(board.colorBoard[y, x]);
                Console.Write(node.Row[x]);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(TetrisConstants.TetrisBlock);
            Console.Write(y);
            Console.WriteLine();
            y++;
            node = node.Next;
        }
        Console.WriteLine("\u25A0\u25A0\u25A0\u25A0\u25A0\u25A0\u25A0\u25A0\u25A0\u25A0\u25A0");

        int cursorX = 25;
        int cursorY = 0;

        Console.SetCursorPosition(cursorX, cursorY++);
        Console.WriteLine($"Score: {score}");
        Console.SetCursorPosition(cursorX, cursorY++);
        Console.WriteLine($"Line Count: {totalLineCleared}");
        Console.SetCursorPosition(cursorX, cursorY++);
        Console.WriteLine($"Next Shape:");
        cursorX = 30;
        Console.SetCursorPosition(cursorX, cursorY++);
        TetrisUtils.SelectColor(board.nextShape.Color);
        foreach (var coord in board.nextShape.Shape)
        {
            Console.SetCursorPosition(cursorX + coord.X, cursorY + coord.Y);
            Console.Write(TetrisConstants.TetrisBlock);
        }
    }
}
