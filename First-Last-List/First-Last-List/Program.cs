using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;

public class Program
{
    static void Main(string[] args)
    {
        FirstLastList<int> list = new FirstLastList<int>();

        list.Add(5);
        list.Add(6);
        list.Add(1);
        list.Add(3);
        list.Add(3);
        list.Add(8);
        list.Add(3);

        Console.WriteLine(list.RemoveAll(150));
        Console.WriteLine(list.RemoveAll(3));
    }
}
