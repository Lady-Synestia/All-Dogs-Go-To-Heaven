using System.Collections.Generic;

/// <summary>
/// Generic Priority Queue type, implemented as a bucket queue.
/// </summary>
/// <typeparam name="T">Data type to store</typeparam>
public class BucketQueue<T>
{
    /*
     * The Bucket Queue holds a dictionary of linked lists where:
     *      - The dictionary's keys correspond to each priority
     *      - The Linked Lists are the 'bucket' of values for each priority
     *
     * To insert, the value is added to the end of the linked list for its priority.
     * The use of a hash table in the form of a dictionary ensures this is an O(1) operation
     *
     * To Dequeue, the first value in the list with the highest priority is extracted.
     * In the best case, this is also an O(1) operation.
     * 
     * As the hash table is unordered, the highest priority key must be stored to ensure
     * O(1) extraction in the best case. However, the Dequeue operation becomes O(n)
     * (where n is the number of keys) when the value extracted is the last item in a bucket,
     * as it has to find the new highest priority.
     *
     * To mitigate the effect on performance this has, the range of possible priorities
     * should not be too large.
     */
    
    
    private Dictionary<int, LinkedList<T>> _buckets = new();
    private int _highestPriority;

    public bool Insert(int priority, T value)
    {
        bool newHighest = false;
        
        // if the priority is not already in the dictionary, a new key-value pair is made,
        // and the priority is compared with the current highest priority
        if (_buckets.TryAdd(priority, new LinkedList<T>()))
        {
            if (priority > _highestPriority)
            {
                _highestPriority = priority;
                newHighest = true;
            }
        }
        _buckets[priority].AddLast(value);
        
        // if the new priority is now the highest, returns true
        return newHighest;
    }

    public T Dequeue()
    {
        // extraction is O(1) in the best case
        T toExtract = _buckets[_highestPriority].First.Value;
        _buckets[_highestPriority].RemoveFirst();

        // finding the new highest priority
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