
public class LinkedList
{

    public int Size { get; private set; }
    public LinkedListNode First { get; set; }
    public LinkedListNode Last { get; set; }

    public LinkedList()
    {
        First = new LinkedListNode(Enumerable.Repeat<char>(TetrisConstants.Empty, 10).ToArray());
        Size++;
        Last = First;
        for (int i = 1; i < TetrisConstants.BoardHeight; i++)
        {
            AddLast(Enumerable.Repeat<char>(TetrisConstants.Empty, 10).ToArray());
        }
    }

    public LinkedList(List<List<char>> listBoard)
    {
        First = new LinkedListNode(listBoard[0].ToArray());
        Size++;
        Last = First;
        for (int i = 1; i < TetrisConstants.BoardHeight; i++)
        {
            AddLast(listBoard[i].ToArray());
        }
    }

    public void InitList()
    {
        First = new LinkedListNode(Enumerable.Repeat<char>(TetrisConstants.Empty, 10).ToArray());
        Size++;
        Last = First;
        for (int i = 1; i < TetrisConstants.BoardHeight; i++)
        {
            AddLast(Enumerable.Repeat<char>(TetrisConstants.Empty, 10).ToArray());
        }
    }

    public char[] ElementAt(int index)
    {
        if (index < 0 || index >= Size)
        {
            throw new ArgumentOutOfRangeException("index");
        }
        LinkedListNode? node = First;
        for (int i = 0; i < index; i++)
        {
            if (node == null)
            {
                throw new Exception();
            }
            node = node.Next;
        }
        if (node == null)
        {
            throw new Exception();
        }
        return node.Row;
    }

    public void AddLast(char[] row)
    {
        if (First != null)
        {
            LinkedListNode node = new LinkedListNode(row);
            Last.Next = node;
            Last = node;
        }
        else
        {
            First = new LinkedListNode(row);
            Last = First;
        }
        Size++;
    }
}

public class LinkedListNode
{
    public char[] Row { get; set; }
    public LinkedListNode? Next { get; set; }
    public LinkedListNode? Prev { get; set; }

    public LinkedListNode()
    {
        Row = [];
    }

    public LinkedListNode(int size)
    {
        Row = new char[size];
    }

    public LinkedListNode(char[] row)
    {
        Row = row;
    }
}