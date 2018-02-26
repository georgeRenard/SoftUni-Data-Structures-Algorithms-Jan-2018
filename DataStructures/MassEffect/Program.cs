using System;
using System.Collections.Generic;
using System.Linq;



class Program
{
    static void Main(string[] args)
    {

        int systemsCount = int.Parse(Console.ReadLine());
        int reportsCount = int.Parse(Console.ReadLine());
        int side = int.Parse(Console.ReadLine());
        List<Point2D> systems = new List<Point2D>();

        for (int i = 0; i < systemsCount; i++)
        {
            string[] lineArgs = Console.ReadLine().Split();
            int x = int.Parse(lineArgs[1]);
            int y = int.Parse(lineArgs[2]);
            systems.Add(new Point2D(x,y));
        }

        KdTree tree = new KdTree();

        tree.BuildFromList(systems);
        tree.EachInOrder(Console.WriteLine);
        for (int i = 0; i < reportsCount; i++)
        {
            string line = Console.ReadLine();
            //Reports
        }

    }
}