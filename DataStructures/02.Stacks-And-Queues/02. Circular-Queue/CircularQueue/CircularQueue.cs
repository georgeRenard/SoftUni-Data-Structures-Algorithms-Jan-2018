using System;
using System.Collections;
using System.Collections.Generic;

public class CircularQueue<T> : IEnumerable<T>
{

    private T[] arr;
    private int head;
    private int tail;

    private const int DefaultCapacity = 4;
    public int Count { get; private set; }
    public int Capacity { get; private set; }

    public CircularQueue(int capacity = DefaultCapacity)
    {
        this.Capacity = capacity;
        this.head = 0;
        this.tail = 0;
        this.Count = 0;
        this.arr = new T[this.Capacity];
    }

    public void Enqueue(T element)
    {
        if(this.Count >= this.Capacity)
        {
            this.Resize();
        }

        this.arr[tail] = element;
        this.tail = (this.tail + 1) % this.Capacity;
        this.Count++;
    }

    private void Resize()
    {
        T[] newArray = new T[this.Capacity * 2];
        this.CopyAllElements(newArray);
        this.Capacity *= 2;
        this.arr = newArray;
    }

    private void CopyAllElements(T[] newArray)
    {
        for (int i = 0; i < this.Count ; i++)
        {
            int index = (i + this.head) % this.Capacity;
            newArray[i] = this.arr[index];
        }
        this.head = 0;
        this.tail = this.Count;
    }

    // Should throw InvalidOperationException if the queue is empty
    public T Dequeue()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        T element = this.arr[this.head];
        this.head = (this.head + 1) % this.Capacity;
        this.Count--;
        
        return element;
    }

    public T[] ToArray()
    {
        T[] newArray = new T[this.Count];
        this.CopyAllElements(newArray);
        return newArray;
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}


public class Example
{
    public static void Main()
    {

        CircularQueue<int> queue = new CircularQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Dequeue();

        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        int first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-7);
        queue.Enqueue(-8);
        queue.Enqueue(-9);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-10);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");
    }
}
