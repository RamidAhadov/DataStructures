using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using DataStructuresDemo.Exceptions;

namespace DataStructuresDemo.List;

[DebuggerTypeProxy(typeof(ICollectionDebugView<>))] //Front: Stack.
[DebuggerDisplay("Count = {Count}")]
public class ListDemo<T> : IEnumerable,ICollection<T>
{
    //Items of list.
    private T[] _data;
    
    //Count of the elements.
    private int _count;
    
    //Capacity of the list.
    //Settable capacity.
    private int _capacity;

    //Default capacity of list.
    //This is the capacity of the list when it is created or cleared.
    private static int _defaultCapacity = 4;
    
    public ListDemo()
    {
        _data = Array.Empty<T>();
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
    public ListDemo(int capacity)
    {
        if (capacity < 0)
        {
            throw new ArgumentOutOfRangeException(capacity.ToString());
        }
        if (capacity == 0)
        {
            _capacity = 0;
            _data = Array.Empty<T>();
        }
        else
        {
            _capacity = capacity;
            _data = new T[capacity];
        }
    }


    [Description("Get current count of elements.")]
    public int Count => _count;

    //List is not readonly.
    public bool IsReadOnly => false;

    public int Capacity => _capacity;

    [Description("To add a new element to the list.")]
    public void Add(T item)
    {
        if (_count < _capacity)
        {
            _data[_count] = item;
            _count++;
        }
        else
        {
            Resized(item);
        }
    }

    public void Clear()
    {
        _data = Array.Empty<T>();
        _capacity = _defaultCapacity;
        _count = 0;
    }

    public void AddRange(ICollection<T> collection)
    {
        int lenght = collection.Count;
        if (lenght + _count > Array.MaxLength)
            throw new ArrayLenghtOutOfTheBoundException(ConstantMessages.ArrayLenghtOutOfTheBound);
        if (collection == null)
            throw new NullReferenceException(ConstantMessages.NullCollectionAdded);
        T[] resizedArray = _data;
        while (_capacity < lenght + _count)
        {
            T[] newArray = Grow(_data);
            _data.CopyTo(newArray, 0);
            resizedArray = newArray;
        }

        collection.CopyTo(resizedArray,_count);
        _data = resizedArray;
        _count += lenght;
    }
    private void Resized(T item)
    {
        T[] newArray = Grow(_data);
        _data.CopyTo(newArray, 0);
        newArray[_count] = item;
        _data = newArray;
        _count++;
    }

    private T[] Grow(T[] array)
    {
        if (_capacity != 0)
        {
            _capacity = array.Length * 2;
            T[] newArray;
            if (_capacity >= Array.MaxLength)
            {
                _capacity = Array.MaxLength;
                newArray = new T[Array.MaxLength];
            }
            else
            {
                newArray = new T[_capacity];
            }

            return newArray;
        }

        return new T[_defaultCapacity];
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (arrayIndex < 0)
            throw new NegativeIndexException(ConstantMessages.NegativeIndex);
        if (array == null)
            throw new NullReferenceException(ConstantMessages.NullCollectionAdded);
        
        Array.Copy(_data,0,array,arrayIndex,_count);
    }

    [Description("To remove an element from list.")]
    public bool Remove(T item)
    {
        int currentCapacity = _data.Length;
        int indexOf = GetIndex(item);
        if (indexOf == -1)
            return false;
        var newArray = Remove(indexOf);
        if ((currentCapacity / 2) >= _count)
        {
            _data = Shrink(newArray);
            return true;
        }

        _data = newArray;
        return true;
    }
    
    private T[] Remove(int index)
    {
        if (index < 0)
            throw new NegativeIndexException(ConstantMessages.NegativeIndex);
        if (_capacity == _count)
        {
            T[] adjustedArray = new T[_capacity]; 
            Array.Copy(_data,0,adjustedArray,0,_count-1);
            _count--;
            return adjustedArray;
        }
        Array.Copy(_data,index+1,_data,index,_count - index);
        //Array.Copy(_data,index +1, adjustedArray);
        _count--;
        return _data;
    }// 1 2 3 4 5 6 7 8
    //  1 2 3 4 5 6 7 8
    //  0 0 0 0 0 0 0 0      3
    //  1 2 3 5 6 7 8 0
    
    private T[] Shrink(T[] array)
    {
        int currentLenght = array.Length;
        bool isEven = currentLenght % 2 == 0;
        if (isEven && currentLenght > 0)
        {
            T[] newArray = new T[currentLenght / 2];
            Array.Copy(array,0,newArray,0,_count);
            _capacity = currentLenght / 2;
            return newArray;
        }

        return array;
    }
    
    bool ICollection<T>.Remove(T item)
    {
        return Remove(item);
    }

    private int GetIndex(T item)
    {
        int index = 0;
        for (int i = 0; i < _count; i++)
        {
            if (Equals(_data[i], item))
            {
                return index;
            }

            index++;
        }

        return -1;
    }

    [Description("Get index of the item.")]
    public int IndexOf(T item)
    {
        return GetIndex(item);
    }
    
    [Description("Get element by index.")]
    public T GetElement(int index)
    {
        if (index < 0)
            throw new NegativeIndexException(ConstantMessages.NegativeIndex);
        if (index > _count - 1)
            throw new IndexOutOfRangeException(ConstantMessages.IndexOutOfBound);
        return _data[index];
    }

    public bool Contains(T item)
    {
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