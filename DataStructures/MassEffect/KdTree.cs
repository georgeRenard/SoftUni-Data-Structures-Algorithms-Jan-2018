using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public class KdTree
{
    private Node root;

    public class Node
    {
        public Node(Point2D point)
        {
            this.Point = point;
        }
        
        public Rectangle Rectangle { get; set; }
        public Point2D Point { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    public Node Root
    {
        get
        {
            return this.root;
        }
    }

    public bool Contains(Point2D point)
    {
        throw new NotImplementedException();
    }

    public void Insert(Point2D point)
    {
        throw new NotImplementedException();
    }

    public void BuildFromList(List<Point2D> systems)
    {
        this.root = this.Build(systems);
    }

    private Node Build(List<Point2D> systems, int depth = 0)
    {

        if (systems.Count == 0)
        {
            return null;
        }

        int axis = depth % 2;
        systems.Sort((x, y) =>
        {
            if (axis == 0)
            {
                return x.X.CompareTo(y.X);
            }
            return x.Y.CompareTo(y.Y);
        });

        int median = systems.Count / 2;
        List<Point2D> left = new List<Point2D>();
        List<Point2D> right = new List<Point2D>();

        for (int i = 0; i < median; i++)
        {
            left.Add(systems[i]);
        }
        for (int i = median+1; i < systems.Count; i++)
        {
            right.Add(systems[i]);
        }

        Node newNode = new Node(systems[median]);
        newNode.Left = this.Build(left, depth + 1);
        newNode.Right = this.Build(right, depth + 1);
        return newNode;
    }

    public void EachInOrder(Action<Point2D> action)
    {
        this.EachInOrder(this.root, action);
    }

    private void EachInOrder(Node node, Action<Point2D> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Point);
        this.EachInOrder(node.Right, action);
    }
}
