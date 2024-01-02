using System.Diagnostics;

namespace DataStructuresDemo.Queue
{
    internal sealed class QueueDebugView<T>
    {
        private readonly Queue<T> _queue;

        public QueueDebugView(Queue<T> queue)
        {
            ArgumentNullException.ThrowIfNull(queue);

            _queue = queue;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public T[] Items
        {
            get
            {
                return _queue.ToArray();
            }
        }
    }
}