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
            throw new EmptyListException(ConstantMessages.EmptyList);
        if (item==null)
            throw new ArgumentNullException(ConstantMessages.NullItem);
        var node = FindNode(item);
        bool result = RemoveNode(node);
        if (!result)
            throw new ItemNotFoundException(ConstantMessages.ItemNotFound);
    }

    //Complete removals
    private bool RemoveNode(LinkedListNodeDemo<T> node)
    {
        return true;
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