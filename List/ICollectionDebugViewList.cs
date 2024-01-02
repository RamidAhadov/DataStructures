using System.Diagnostics;

namespace DataStructuresDemo.List;

internal sealed class ICollectionDebugView<T>
{
    private readonly ICollection<T> _collection;

    public ICollectionDebugView(ICollection<T> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);

        _collection = collection;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
    public T[] Items
    {
        get
        {
            T[] items = new T[_collection.Count];
            _collection.CopyTo(items, 0);
            return items;
        }
    }
}