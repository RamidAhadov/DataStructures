namespace DataStructuresDemo.LinkedList;

public class LinkedListNodeDemo<T>
{
    public T Data;
    public LinkedListNodeDemo<T> Next;

    public LinkedListNodeDemo(T data)
    {
        Data = data;
        Next = null;
    }
}