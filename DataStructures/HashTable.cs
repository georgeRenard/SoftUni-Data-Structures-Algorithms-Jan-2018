using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
{

    private const float LoadFactor = 0.7f;
    private const int DefaultCapacity = 16;

    private LinkedList<KeyValue<TKey, TValue>>[] elements;
    public int Count { get; private set; }

    public int Capacity
    {
        get { return this.elements.Length; }
    }

    public HashTable(int capacity = DefaultCapacity)
    {
        this.elements = new LinkedList<KeyValue<TKey, TValue>>[capacity];
    }

    public void Add(TKey key, TValue value)
    {
        GrowIfNeeded();
        int index = Math.Abs(key.GetHashCode()) % this.Capacity;

        if (this.elements[index] == null)
        {
            this.elements[index] = new LinkedList<KeyValue<TKey, TValue>>();
        }

        foreach (var item in this.elements[index])
        {
            if (item.Key.Equals(key))
            {
                throw new ArgumentException();
            }
        }
        
        KeyValue<TKey, TValue> kvp = new KeyValue<TKey, TValue>(key, value);
        this.elements[index].AddLast(kvp);
        this.Count++;
    }

    public bool AddOrReplace(TKey key, TValue value)
    {

        GrowIfNeeded();
        int index = Math.Abs(key.GetHashCode()) % this.Capacity;

        if (this.elements[index] == null)
        {
            this.elements[index] = new LinkedList<KeyValue<TKey, TValue>>();
        }

        foreach (var item in this.elements[index])
        {
            if (item.Key.Equals(key))
            {
                item.Value = value;
                return true;
            }
        }

        KeyValue<TKey, TValue> kvp = new KeyValue<TKey, TValue>(key, value);
        this.elements[index].AddLast(kvp);
        this.Count++;
        return false;
    }

    public TValue Get(TKey key)
    {
        KeyValue<TKey, TValue> kvp = this.Find(key);

        if (kvp == null)
        {
            throw new KeyNotFoundException();
        }

        return kvp.Value;
    }

    public TValue this[TKey key]
    {
        get => this.Get(key);
        set => this.AddOrReplace(key, value);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        KeyValue<TKey, TValue> kvp = this.Find(key);

        if (kvp == null)
        {
            value = default(TValue);
            return false;
        }

        value = kvp.Value;
        return true;
    }

    public KeyValue<TKey, TValue> Find(TKey key)
    {
        int index = Math.Abs(key.GetHashCode()) % this.Capacity;

        if (this.elements[index] != null)
        {
            foreach (var item in this.elements[index])
            {
                if (item.Key.Equals(key))
                {
                    return item;
                }
            }
        }
        return null;
    }

    public bool ContainsKey(TKey key)
    {
        return this.Find(key) != null;
    }

    public bool Remove(TKey key)
    {
        int index = Math.Abs(key.GetHashCode()) % this.Capacity;

        if (this.elements[index] == null)
        {
            return false;
        }

        KeyValue<TKey, TValue> kvpToRemove = null;
        foreach (var item in this.elements[index])
        {
            if (item.Key.Equals(key))
            {
                kvpToRemove = item;
            }
        }

        if (kvpToRemove == null)
        {
            return false;
        }

        this.elements[index].Remove(kvpToRemove);
        this.Count--;
        return true;
    }

    public void Clear()
    {
        this.elements = new LinkedList<KeyValue<TKey, TValue>>[DefaultCapacity];
        this.Count = 0;
    }

    public IEnumerable<TKey> Keys
    {
        get
        {
            foreach (var kvp in this.elements.Where(x => x != null))
            {
                foreach (var item in kvp)
                {
                    yield return item.Key;
                }
            }
        }
    }

    public IEnumerable<TValue> Values
    {
        get
        {
            foreach (var kvp in this.elements.Where(x => x!=null))
            {
                foreach (var item in kvp)
                {
                    yield return item.Value;
                }
            }
        }
    }

    private void GrowIfNeeded()
    {
        float loadFactor = (float)(this.Count+1) / this.Capacity;

        if (loadFactor >= LoadFactor)
        {
            Grow();
        }
    }

    private void Grow()
    {
        HashTable<TKey,TValue> newTable = 
            new HashTable<TKey,TValue>(this.Capacity*2);

        foreach (var element in this.elements.Where(x => x!=null))
        {
            foreach (var kvp in element)
            {
                newTable.Add(kvp.Key, kvp.Value);
            }
        }

        this.elements = newTable.elements;
    }

    public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
    {
        foreach (var element in this.elements.Where(x => x!=null))
        {
            foreach (var kvp in element)
            {
                yield return kvp;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
