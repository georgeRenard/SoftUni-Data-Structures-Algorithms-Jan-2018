using System;
using System.Collections.Generic;
using System.Linq;


class Program
{

    static Dictionary<int, Tree<int>> tree
        = new Dictionary<int, Tree<int>>();

    static void Main(string[] args)
    {

        int numberOfNodes = int.Parse(Console.ReadLine());

        for (int i = 0; i < numberOfNodes - 1; i++)
        {

            int[] nodes = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            int parent = nodes[0];
            int child = nodes[1];

            if (!tree.ContainsKey(parent))
            {
                tree.Add(parent, new Tree<int>(parent));
            }

            if (!tree.ContainsKey(child))
            {
                tree.Add(child, new Tree<int>(child));
            }

            Tree<int> parentNode = tree[parent];
            Tree<int> childNode = tree[child];
            parentNode.Children.Add(childNode);
            childNode.Parent = parentNode;
        }

        Tree<int> root = tree.FirstOrDefault(x => x.Value.Parent == null).Value;

        //root.Print();

        int sum = int.Parse(Console.ReadLine());

        //List<Tree<int>> leafs = tree.Values
        //    .Where(x => x.Children.Count == 0)
        //    .ToList();

        //Console.WriteLine("Paths of sum {0}:", sum);
        //DFS(root, sum);

        Console.WriteLine("Subtrees of sum {0}:", sum);
        SubtreeDFS(root, sum);


    }

    static int SubtreeDFS(Tree<int> node, int sum)
    {

        int currentSum = node.Value;

        foreach(var child in node.Children)
        {
            currentSum += SubtreeDFS(child, sum);
        }

        if(sum == currentSum)
        {
            List<int> subtree = new List<int>();
            GetSubtree(node, subtree);
            Console.WriteLine(string.Join(" ", subtree));
        }

        return currentSum;
    }

    static void GetSubtree(Tree<int> node, List<int> result)
    {

        result.Add(node.Value);
        foreach(var child in node.Children)
        {
            GetSubtree(child, result);
        }

    }

    static void DFS(Tree<int> node, int targetSum, int sum = 0)
    {

        sum += node.Value;
        if (sum == targetSum)
        {
            PrintPath(node);
        }

        foreach (var child in node.Children)
        {
            DFS(child, targetSum, sum);
        }

    }

    private static void PrintPath(Tree<int> node)
    {

        Stack<int> path = new Stack<int>();
        Tree<int> start = node;
        path.Push(start.Value);
        while (start.Parent != null)
        {
            start = start.Parent;
            path.Push(start.Value);
        }

        Console.WriteLine(string.Join(" ", path));
    }

    class Tree<T>
    {

        public T Value { get; set; }
        public List<Tree<T>> Children { get; set; }
        public Tree<T> Parent { get; set; }

        public Tree(T value, params Tree<T>[] children)
        {
            this.Value = value;
            this.Children = new List<Tree<T>>(children);
        }

        public void Print(int indent = 0)
        {

            Console.WriteLine($"{new string(' ', indent)}{this.Value}");
            foreach (var child in this.Children)
            {
                child.Print(indent + 2);
            }

        }

    }
}