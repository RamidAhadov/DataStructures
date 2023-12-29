using System.Collections;
using System.Diagnostics;
using ListDemo.Exceptions;

namespace ListDemo.Queue;

[DebuggerTypeProxy(typeof(QueueDebugView<>))]
[DebuggerDisplay("Count = {Count}")]
public class QueueDemo<T>:IEnumerable<T>,ICollection
{
    //Items of the queue
    private T[] _data;
    
    //Number of the items of queue
    private int _count;

    //Capacity of queue
    private int _capacity;

    //Index of head item of queue
    private int _head;

    //Index of tail item of queue
    private int _tail;

    private const int _defaultCapacity = 4;
    
    public int Count => _count;

    public int Lenght => _capacity;

    public bool IsSynchronized => false;
    
    public object SyncRoot => this;

    public QueueDemo()
    {
        _data = Array.Empty<T>();
    }

    public QueueDemo(int capacity)
    {
        _data = new T[capacity];
        _capacity = capacity;
    }
    
    // Common methods
    // Enqueue - adds an object to end of the queue
    // Dequeue - returns an object at beginning of the queue and removes it
    // Peek - returns an object at beginning of the queue

    public void Enqueue(T item)
    {
        if (item == null)
            throw new NullReferenceException(ConstantMessages.NullItem);
        if (_count < _capacity)
        {
            _data[_count] = item;
            _count++;
        }
        else
        {
            T[] array = Resized();
            array[_count] = item;
            _count++;
        }

        _tail = _count-1;
        
        if (_count == 1)
        {
            _head = _tail;
        }
    }

    private T[] Resized()
    {
        if (_capacity != 0)
        {
            _capacity *= 2;
            if (_capacity > Array.MaxLength)
            {
                _capacity = Array.MaxLength;
            }

            T[] resizedArray = new T[_capacity];
            Array.Copy(_data,0,resizedArray,0,_count);
            _data = resizedArray;
            return _data;
        }

        _data = new T[_defaultCapacity];
        _capacity = _defaultCapacity;
        return _data;
    }
    
    public T Dequeue()
    {
        if (_capacity == 0)
            throw new NullReferenceException(ConstantMessages.EmptyArray);
        return Remove(_head);
    }

    public bool TryDequeue()
    {
        if (_capacity == 0)
            return false;
        try
        {
            Remove(_head);
        }
        catch (DequeueException e)
        {
            Console.WriteLine(e);
            return false;
        }

        return true;
    }
    private T Remove(int index)
    {
        T dequeuedItem = _data[index];
        T[] adjustedArray = new T[_capacity];
        Array.Copy(_data,_head+1,adjustedArray,0,_count);
        _count--;
        if ((_capacity/2) >= _count)
        {
            Shrink(adjustedArray);
        }

        return dequeuedItem;
    }

    private void Shrink(T[] array)
    {
        int currentCapacity = array.Length;
        bool isEven = currentCapacity % 2 == 0;
        if (isEven && currentCapacity > 0)
        {
            T[] shrunkArray = new T[currentCapacity / 2];
            Array.Copy(array,0,shrunkArray,0,_count);
        }
    }

    public T Peek()
    {
        if (_capacity == 0)
            throw new NullReferenceException(ConstantMessages.EmptyArray);
        var item = _data[_head];
        return item;
    }

    public void Clear()
    {
        if (_capacity == 0)
        {
            
        }
        else
        {
            _data = Array.Empty<T>();
            _capacity = 0;
            _count = 0;
            _head = _tail = 0;
        }
    }

    public bool Contains(T item)
    {
        if (item == null)
            return false;
        if (_capacity == 0)
            return false;
        for (int i = 0; i < _count; i++)
        {
            if (Equals(_data[i],item))
            {
                return true;
            }
        }

        return false;
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _count; i++)
        {
            yield return _data[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void CopyTo(Array array, int index)
    {
        if (_count != 0)
        {
            Array.Copy(_data,0,array,index,_count);
        }
    }
}