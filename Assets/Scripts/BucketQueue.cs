using System.Collections.Generic;

public class BucketQueue<T>
{
    private Dictionary<int, LinkedList<T>> _buckets = new();
    private int _highestPriority;

    public bool Insert(int priority, T value)
    {
        bool newHighest = false;
        if (_buckets.TryAdd(priority, new LinkedList<T>()))
        {
            if (priority > _highestPriority)
            {
                _highestPriority = priority;
                newHighest = true;
            }
        }
        _buckets[priority].AddLast(value);
        return newHighest;
    }

    public T Dequeue()
    {
        T toExtract = _buckets[_highestPriority].First.Value;
        _buckets[_highestPriority].RemoveFirst();

        if (_buckets[_highestPriority].Count == 0)
        {
            _buckets.Remove(_highestPriority);
            _highestPriority = 0;
            if (!IsEmpty())
            {
                foreach (int key in _buckets.Keys)
                {
                    if (key > _highestPriority)
                    {
                        _highestPriority = key;
                    }
                }
            }
        }
        return toExtract;
    }

    public T Peek()
    {
        return _buckets[_highestPriority].First.Value;
    }

    public bool IsEmpty()
    {
        return _buckets.Count == 0;
    }

    public void Clear()
    {
        _buckets.Clear();
        _highestPriority = 0;
    }
}