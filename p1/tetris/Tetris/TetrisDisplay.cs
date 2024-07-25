


public class TetrisDisplay
{
    public List<string>? HighScores { get; set; }
    public TetrisBoard board { get; set; }

    public TetrisDisplay(TetrisBoard board)
    {
        this.board = board;
    }

    public void InitDisplay()
    {
        Console.CursorVisible = false;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Display(0, 0, 0);
    }

    public void UndoShapeDisplay()
    {
        Coordinate pos;
        foreach (var coord in board.CurrentShape.Shapes)
        {
            pos = coord + board.CurrentPos;
            Console.SetCursorPosition(pos.X + 1, pos.Y);
            Console.Write(TetrisConstants.Empty);
        }
    }

    public void DisplayShape(int level, int score, int totalLineCleared)
    {
        Coordinate pos;
        ColorHelper.SelectColor(board.CurrentShape.Color);
        foreach (var coord in board.CurrentShape.Shapes)
        {
            pos = coord + board.CurrentPos;
            Console.SetCursorPosition(pos.X + 1, pos.Y);
            Console.Write(TetrisConstants.TetrisBlock);
        }

    }
    public void Display(int level, int score, int totalLineCleared)
    {
        Console.Clear();
        DisplayBoard(level, score, totalLineCleared);
        DisplayInfo(level, score, totalLineCleared);
    }


    private void DisplayBoard(int level, int score, int totalLineCleared)
    {
        int y = 0;
        LinkedListNode? node = board.Board.First;
        while (node != null)
        {
            ColorHelper.SelectColor(Color.DefaultColor);
            Console.Write(TetrisConstants.TetrisBlock);
            for (int x = 0; x < TetrisConstants.BoardWidth; x++)
            {
                ColorHelper.SelectColor(board.ColorBoard[y, x]);
                Console.Write(node.Row[x]);
            }
            ColorHelper.SelectColor(Color.DefaultColor);
            Console.Write(TetrisConstants.TetrisBlock);
            Console.Write(y + 1);
            Console.WriteLine();
            y++;
            node = node.Next;
        }
        Console.WriteLine("\u25A0\u25A0\u25A0\u25A0\u25A0\u25A0\u25A0\u25A0\u25A0\u25A0\u25A0\u25A0");
    }

    private void DisplayInfo(int level, int score, int totalLineCleared)
    {
        int cursorX = 15;
        int cursorY = 0;

        Console.SetCursorPosition(cursorX, cursorY++);
        Console.WriteLine($"Level: {level + 1}");
        Console.SetCursorPosition(cursorX, cursorY++);
        Console.WriteLine($"Score: {score}");
        Console.SetCursorPosition(cursorX, cursorY++);
        Console.WriteLine($"Line Count: {totalLineCleared}");
        Console.SetCursorPosition(cursorX, cursorY++);
        Console.WriteLine($"Next Shape:");
        cursorX += 4;
        Console.SetCursorPosition(cursorX, cursorY++);
        ColorHelper.SelectColor(board.NextShape.Color);
        foreach (var coord in board.NextShape.Shapes)
        {
            Console.SetCursorPosition(cursorX + coord.X, cursorY + coord.Y);
            Console.Write(TetrisConstants.TetrisBlock);
        }
    }
    public void DisplayHighScores(int cursorX, int cursorY)
    {
        Console.SetCursorPosition(cursorX, cursorY++);
        ColorHelper.SelectColor(Color.DefaultColor);
        Console.WriteLine(String.Format("{0,-6}|{1, -6}|{2, -6}|{3,-6}", "Score", "Lines", "   Name   ", "Date"));
        if (HighScores == null)
        {
            return;
        }
        foreach (string str in HighScores)
        {
            Console.SetCursorPosition(cursorX, cursorY++);
            Console.WriteLine(str);
        }
    }

    public void DisplayGameOver(int level, int score, int totalLineCleared)
    {
        int cursorX = 15;
        int cursorY = 9;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(cursorX, cursorY);
        Console.WriteLine("GAME OVER!");
        Console.SetCursorPosition(cursorX, cursorY + 1);
        Console.WriteLine($"Your Score is {score}");
    }

    public void DisplaySave()
    {
        int cursorX = 15;
        int cursorY = 9;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(cursorX, cursorY);
        Console.WriteLine("SAVED GAME!");
    }

    public void DisplayQuit()
    {
        int cursorX = 15;
        int cursorY = 9;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(cursorX, cursorY);
        Console.WriteLine("QUIT GAME!");
    }

    public void DisplayPaused()
    {
        int cursorX = 15;
        int cursorY = 9;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(cursorX, cursorY++);
        Console.WriteLine("--PAUSED--");
        Console.SetCursorPosition(cursorX, cursorY++);
        Console.WriteLine("--Press Any Key--");

    }
}