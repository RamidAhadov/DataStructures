using System.Collections;
using System.Diagnostics;
using DataStructuresDemo.Exceptions;

namespace DataStructuresDemo.LinkedList;

[DebuggerDisplay("Count={Count}")]
public class LinkedListDemo<T>:IEnumerable<T>,ICollection
{
    private int _count;
    private LinkedListNodeDemo<T> _head;
    private LinkedListNodeDemo<T> _tail;

    public LinkedListDemo()
    {
        _head = null;
    }

    public int Count => _count;

    public T First => _head.Data;

    public T Last => _tail.Data;

    public bool IsSynchronized => false;
    
    public object SyncRoot => this;
    
    
    public void AddLast(T item)
    {
        LinkedListNodeDemo<T> newNode = new LinkedListNodeDemo<T>(item);
        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
            _count++;
        }
        else
        {
            _tail.Next = newNode;
            _tail = newNode;
            _count++;
        }
    }

    public void AddFirst(T item)
    {
        LinkedListNodeDemo<T> newNode = new LinkedListNodeDemo<T>(item);
        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
            _count++;
        }
        else
        {
            newNode.Next = _head;
            _head = newNode;
            _count++;
        }
    }
    
    public void AddAfter(T item,T addedItem)
    {
        if (addedItem==null)
            throw new NullReferenceException(ConstantMessages.NullItem);
        if (_head == null)
            throw new EmptyListException(ConstantMessages.EmptyList);
        
        LinkedListNodeDemo<T> newNode = new LinkedListNodeDemo<T>(addedItem);
        LinkedListNodeDemo<T> currentNode = FindNode(item);
        InsertNodeAfter(currentNode,newNode);
        _count++;
    }

    private LinkedListNodeDemo<T> FindNode(T item)
    {
        LinkedListNodeDemo<T> currentNode = _head;
        while (!Equals(currentNode.Data,item))
        {
            currentNode = currentNode.Next ?? throw new ItemNotFoundException(ConstantMessages.ItemNotFound);
        }

        return currentNode;
    }
    
    private LinkedListNodeDemo<T> FindPreviousNode(T item)
    {
        LinkedListNodeDemo<T> currentNode = _head;
        LinkedListNodeDemo<T> previousNode = null;
        while (!Equals(currentNode.Data,item))
        {
            previousNode = currentNode;
            currentNode = currentNode.Next ?? throw new ItemNotFoundException(ConstantMessages.ItemNotFound);
        }

        return previousNode;
    }

    private LinkedListNodeDemo<T> FindPreviousNode(LinkedListNodeDemo<T> node)
    {
        LinkedListNodeDemo<T> currentNode = _head;
        LinkedListNodeDemo<T> previousNode = null;
        while (!Equals(currentNode,node))
        {
            previousNode = currentNode;
            currentNode = currentNode.Next ?? throw new ItemNotFoundException(ConstantMessages.ItemNotFound);
        }

        return previousNode;
    }

    private void InsertNodeAfter(LinkedListNodeDemo<T> node,LinkedListNodeDemo<T> insertedNode)
    {
        insertedNode.Next = node.Next;
        node.Next = insertedNode;
    }

    public void AddBefore(T item, T addedItem)
    {
        if (addedItem==null)
            throw new NullReferenceException(ConstantMessages.NullItem);
        if (_head == null)
            throw new EmptyListException(ConstantMessages.EmptyList);
        
        LinkedListNodeDemo<T> newNode = new LinkedListNodeDemo<T>(addedItem);
        LinkedListNodeDemo<T> previousNode = FindPreviousNode(item);

        switch (previousNode)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            case null:
                newNode.Next = _head;
                _head = newNode;
                break;
            default:
                InsertNodeAfter(previousNode, newNode);
                break;
        }

        _count++;
    }

    public void Remove(T item)
    {
        if (_head == null)
            throw new EmptyListException(message:ConstantMessages.EmptyList);
        if (item == null)
            throw new ArgumentNullException(ConstantMessages.NullItem);
        var node = FindNode(item);
        if (Equals(node,_head)) RemoveFirstNode();
        else if(Equals(node,_tail)) RemoveLastNode();
        else
        {
            bool result = RemoveNode(node);
            _count--;
            if (!result)
                throw new ItemNotFoundException(ConstantMessages.ItemNotFound);
        }
    }

    public T RemoveFirst()
    {
        if (_head == null)
            throw new EmptyListException(ConstantMessages.EmptyList);
        var removedItem = RemoveFirstNode();
        return removedItem;
    }

    private T RemoveFirstNode()
    {
        var removedItem = _head.Data;
        _head = _head.Next;
        _count--;
        if (_count == 0)
        {
            _tail = null;
        }
        return removedItem;
    }

    public T RemoveLast()
    {
        if (_head == null)
            throw new EmptyListException(ConstantMessages.EmptyList);
        var removedItem = RemoveLastNode();
        return removedItem;
    }

    private T RemoveLastNode()
    {
        var removedItem = _tail.Data;
        if (_count == 1)
        {
            _head = null;
            _tail = null;
            _count = 0;
            return removedItem;
        }
        var previousNode = FindPreviousNode(_tail);
        previousNode.Next = null;
        _tail = previousNode;
        _count--;
        
        return removedItem;
    }
    
    private bool RemoveNode(LinkedListNodeDemo<T> node)
    {
        LinkedListNodeDemo<T> currentNode = _head;
        LinkedListNodeDemo<T> previousNode = null;
        try
        {
            while (currentNode != node)
            {
                previousNode = currentNode;
                currentNode = currentNode.Next;
            }

            previousNode.Next = currentNode.Next;
            _head = previousNode;

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public void Clear()
    {
        if (_head != null)
        {
            _head = null;
            _count = 0;
        }
    }

    public bool Contains(T item)
    {
        if (item == null)
            throw new ArgumentNullException(ConstantMessages.NullItem);
        foreach (var i in this)
        {
            if (Equals(i,item))
            {
                return true;
            }
        }

        return false;
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        LinkedListNodeDemo<T> currentNode = _head;
        if (_head == null)
            throw new EmptyListException(ConstantMessages.EmptyList);
        while (currentNode != null)
        {
            yield return currentNode.Data;
            currentNode = currentNode.Next;
        }
        
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void CopyTo(Array array, int index)
    {
        T[] listArray = new T[_count];
        int count = 0;
        foreach (var item in this)
        {
            listArray[count] = item;
            count++;
        }
        Array.Copy(listArray,0,array,index,_count);
    }
}