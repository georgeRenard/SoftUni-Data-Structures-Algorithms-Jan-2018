using System;

public class HeapExample
{
    static void Main()
    {
        Console.WriteLine("Created an empty heap.");
        var heap = new BinaryHeap<int>();
        heap.Insert(12);
        heap.Insert(7);
        heap.Insert(9);
        heap.Insert(4);
        heap.Insert(1);
        
        int[] arr = new int[]{1, 8, 4, 12, 34, 2, 5};

        Heap<int>.Sort(arr);
        Console.WriteLine(string.Join(" ", arr));
    }
}
