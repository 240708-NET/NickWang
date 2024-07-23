using System.Collections;

public class TetrisLinkedList
{

    public int Size { get; private set; }
    public TetrisLinkedListNode First { get; set; }
    public TetrisLinkedListNode Last { get; set; }

    public TetrisLinkedList()
    {
        First = new TetrisLinkedListNode(Enumerable.Repeat<char>(TetrisConstants.Empty, 10).ToArray());
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
        TetrisLinkedListNode node = First;
        for (int i = 0; i < index; i++)
        {
            if (node.Next != null)
            {

                node = node.Next;
            }
            else
            {
                throw new NullReferenceException("Nullptr");
            }
        }
        return node.Row;
    }

    public void AddLast(char[] row)
    {
        if (First != null)
        {
            TetrisLinkedListNode node = new TetrisLinkedListNode(row);
            Last.Next = node;
            Last = node;
        }
        else
        {
            First = new TetrisLinkedListNode(row);
            Last = First;
        }
        Size++;
    }
}

public class TetrisLinkedListNode
{
    public char[] Row { get; set; }
    public TetrisLinkedListNode? Next { get; set; }
    public TetrisLinkedListNode? Prev { get; set; }

    public TetrisLinkedListNode()
    {
        Row = [];
    }

    public TetrisLinkedListNode(int size)
    {
        Row = new char[size];
    }

    public TetrisLinkedListNode(char[] row)
    {
        Row = row;
    }
}