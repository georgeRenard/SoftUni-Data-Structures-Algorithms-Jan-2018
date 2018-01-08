using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

public class ArrayList<T> : IEnumerable<T>
{

    private T[] arr;

    public int Count { get; set; }
    public int Capacity { get; set; }

    public ArrayList(int capacity = 2)
    {
        this.arr = new T[capacity];
        this.Capacity = capacity;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            return this.arr[index];
        }

        set
        {
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            this.arr[index] = value;
        }
    }

    public void Add(T item)
    {
        if (this.Count + 1 > this.Capacity)
        {
            this.Grow();
        }
        this.arr[this.Count] = item;
        this.Count++;
    }

    public T RemoveAt(int index)
    {
        T item = this[index];
        this[index] = default(T);
        this.ShiftLeft(index);
        if (this.Count - 1 < this.Capacity / 3)
        {
            this.Shrink();
        }
        this.Count--;
        return item;
    }

    private void Grow()
    {
        T[] newArr = new T[this.Capacity * 2];
        this.Capacity *= 2;
        Array.Copy(this.arr, newArr, this.Count);
        this.arr = newArr;
    }

    private void Shrink()
    {
        T[] newArr = new T[this.Capacity / 2];
        this.Capacity /= 2;
        Array.Copy(this.arr, newArr, this.Count);
        this.arr = newArr;
    }

    private void ShiftLeft(int index)
    {
        for (int i = index; i < this.Count - 1; i++)
        {
            this.arr[i] = this.arr[i + 1];
        }

    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in this.arr)
        {
            yield return item;
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
