using System.Collections;
using System.Diagnostics;
using DataStructuresDemo.Exceptions;

namespace DataStructuresDemo.Stack;

[DebuggerTypeProxy(typeof(StackDebugView<>))]
[DebuggerDisplay("Count = {Count}")]
public class StackDemo<T>:IEnumerable<T>,ICollection
{
    //Items of stack
    private T[] _data;
    
    //Count of the current stack;
    private int _count;

    //Capacity of stack
    private int _capacity;

    private const int _defaultCapacity = 4;
    
    public int Count => _count;
    
    public int Lenght => _capacity;
    
    public bool IsSynchronized => false;
    
    public object SyncRoot => this;

    // There are in the stack 3 common methods : Push, Pop, Peek
    // Peek - Returns element at top of the stack without remove it.
    // Pop - Return element at top of the stack and removes it.
    // Push - Adds an element to top of stack.

    public StackDemo()
    {
        _data = Array.Empty<T>();
    }

    public StackDemo(int capacity)
    {
        _capacity = capacity;
    }

    public void Push(T item)
    {
        if (_capacity > _count)
        {
            _data[_count] = item;
        }
        else
        {
            var resizedArray = Grow(_data);
            Array.Copy(_data,resizedArray,_count);
            _data = resizedArray;
            _data[_count] = item;
        }
        _count++;
    }

    private T[] Grow(T[] array)
    {
        if (_capacity != 0)
        {
            int currentCapacity = array.Length;
            _capacity = currentCapacity * 2;
            T[] resizedArray;
            if (_capacity * 2 > Array.MaxLength)
            {
                resizedArray = new T[Array.MaxLength];
                _capacity = Array.MaxLength;
            }
            else
            {
                resizedArray = new T[_capacity];
            }

            return resizedArray;
        }

        _capacity = _defaultCapacity;
        return new T[_defaultCapacity];
    }
    
    public T? Peek()
    {
        if (_count == 0)
            throw new InvalidOperationException(ConstantMessages.EmptyArray);
        return _data[_count - 1];
    }

    public T Pop()
    {
        if (_count == 0)
            throw new InvalidOperationException(ConstantMessages.EmptyArray);
        
        T item = Remove();
        _count--;
        
        if (_count <= _capacity / 2)
        {
            Shrink(_data);
            return item;
        }

        return item;
    }

    private T Remove()
    {
        var item = _data[_count - 1];
        T[] adjustedArray = new T[_capacity];
        Array.Copy(_data,0,adjustedArray,0,_count-1);
        _data = adjustedArray;
        return item;
    }
    
    private void Shrink(T[] array)
    {
        int lenght = array.Length;
        bool isEven = lenght % 2 == 0;
        if (isEven)
        {
            T[] resizedArray = new T[lenght / 2];
            Array.Copy(array,resizedArray,_count);
            _capacity = lenght / 2;
        }
    }

    public void CopyTo(Array array, int index)
    {
        if (index < 0)
            throw new NegativeIndexException(ConstantMessages.NegativeIndex);
        if (array == null)
            throw new NullReferenceException(ConstantMessages.NullCollectionAdded);
        
        Array.Copy(_data,0,array,index,_count);
    }
    
    public bool IsReadOnly => false;
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

    public void Clear()
    {
        _data = Array.Empty<T>();
        _capacity = _defaultCapacity;
        _count = 0;
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
}