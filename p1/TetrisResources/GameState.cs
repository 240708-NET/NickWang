namespace TetrisResources;

public class GameState
{
    public int PlayerId { get; set; }
    public string PlayerName { get; set; }
    public int Level { get; set; }
    public int Score { get; set; }
    public int TotalLineCleared { get; set; }
    public int RandSeed { get; set; }
    public int RandCount { get; set; }
    public Coordinate CurrentPos { get; set; }
    public Shape CurrentShape { get; set; }
    public Shape NextShape { get; set; }
    public List<List<char>> Board { get; set; }
    public List<List<Color>> ColorBoard { get; set; }

    public GameState(int playerId, string playerName, int level, int score, int totalLineCleared, Coordinate currentPos, Shape currentShape, Shape nextShape, List<List<char>> board, List<List<Color>> colorBoard)
    {
        PlayerId = playerId;
        PlayerName = playerName;
        Level = level;
        Score = score;
        TotalLineCleared = totalLineCleared;
        CurrentPos = currentPos;
        CurrentShape = currentShape;
        NextShape = nextShape;
        Board = board;
        ColorBoard = colorBoard;
    }

    public static List<List<char>> LinkedListBoardToListBoard(LinkedList linkedList)
    {
        List<List<char>> listBoard = new List<List<char>>();
        LinkedListNode? node = linkedList.First;
        while (node != null)
        {
            List<char> row = new List<char>();
            for (int i = 0; i < TetrisConstants.BoardWidth; i++)
            {
                row.Add(node.Row[i]);
            }
            listBoard.Add(row);
            node = node.Next;
        }
        return listBoard;
    }

    public static LinkedList ListBoardToLinkedList(List<List<char>> listBoard)
    {
        return new LinkedList(listBoard);
    }

    public static List<List<Color>> ColorBoardToListColorBoard(Color[,] colors)
    {
        List<List<Color>> colorBoard = new List<List<Color>>();

        for (int i = 0; i < TetrisConstants.BoardHeight; i++)
        {
            List<Color> row = new List<Color>();
            for (int j = 0; j < TetrisConstants.BoardWidth; j++)
            {
                row.Add(colors[i, j]);
            }
            colorBoard.Add(row);
        }
        return colorBoard;
    }

    public static Color[,] ListColorBoardToColorBoard(List<List<Color>> colorBoard)
    {
        Color[,] colors = new Color[TetrisConstants.BoardHeight, TetrisConstants.BoardWidth];
        for (int i = 0; i < colorBoard.Count; i++)
        {
            for (int j = 0; j < colorBoard[i].Count; j++)
            {
                colors[i, j] = colorBoard[i][j];
            }
        }
        return colors;
    }
}