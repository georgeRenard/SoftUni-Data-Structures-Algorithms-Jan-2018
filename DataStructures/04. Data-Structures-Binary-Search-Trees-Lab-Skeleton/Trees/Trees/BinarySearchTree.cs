using System;
using System.Collections.Generic;

public class BinarySearchTree<T> where T : IComparable<T>
{

    private Node root;
    private Node current;

    public BinarySearchTree()
    {
        this.root = null;
    }

    private BinarySearchTree(Node node)
    {
        this.Copy(node);
    }

    private void Copy(Node node)
    {

        if (node == null)
        {
            return;
        }

        this.Insert(node.Value);
        this.Copy(node.Left);
        this.Copy(node.Right);
    }

    public void Insert(T value)
    {
        this.root = this.Insert(this.root, value);
        //if (this.root == null)
        //{
        //    this.root = new Node(value);
        //    return;
        //}
        //
        //Node parent = null;
        //Node current = this.root;
        //
        //while (current != null)
        //{
        //
        //    int compare = current.Value.CompareTo(value);
        //
        //    if (compare > 0)
        //    {
        //        // current.Value > value
        //        parent = current;
        //        current = current.Left;
        //    }
        //    else if (compare < 0)
        //    {
        //        // current.Value < value
        //        parent = current;
        //        current = current.Right;
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}
        //
        //Node newNode = new Node(value);
        //if (parent.Value.CompareTo(value) > 0)
        //{
        //    // parent.Value > value
        //    parent.Left = newNode;
        //}
        //else
        //{
        //    // parent.Value < value
        //    parent.Right = newNode;
        //}
    }

    private Node Insert(Node node, T value)
    {

        if (node == null)
        {
            return new Node(value);
        }

        int compare = node.Value.CompareTo(value);

        if (compare > 0)
        {
            node.Left = this.Insert(node.Left, value);
        }
        else if(compare < 0)
        {
            node.Right = this.Insert(node.Right, value);
        }

        return node;
    }

    public bool Contains(T value)
    {

        Node current = this.root;

        while (current != null)
        {

            int compare = current.Value.CompareTo(value);

            if (compare > 0)
            {
                // current.Value > value
                current = current.Left;
            }
            else if (compare < 0)
            {
                // current.Value < value
                current = current.Right;
            }
            else
            {
                return true;
            }
        }

        return false;
    }

    public void DeleteMin()
    {

        if(this.root == null)
        {
            return;
        }

        if (this.root.Left == null && this.root.Right == null)
        {
            this.root = null;
            return;
        }

        Node parent = null;
        Node current = this.root;

        while (current.Left != null)
        {
            parent = current;
            current = current.Left;
        }

        if (current.Right != null)
        {
            parent.Left = current.Right;
        }
        else
        {
            parent.Left = null;
        }

    }

    public BinarySearchTree<T> Search(T item)
    {

        Node current = this.root;

        while (current != null)
        {

            int compare = current.Value.CompareTo(item);
            if (compare > 0)
            {
                //current.Value > item
                current = current.Left;
            }
            else if (compare < 0)
            {
                //current.Value < item
                current = current.Right;
            }
            else if(compare == 0)
            {
                // current.Value == item
                return new BinarySearchTree<T>(current);
            }

        }

        return null;
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {

        List<T> result = new List<T>();

        this.Range(this.root, result, startRange, endRange);

        return result;
    }

    private void Range(Node node, List<T> result,T start,T end)
    {

        if (node == null)
        {
            return;
        }

        int compareLow = node.Value.CompareTo(start);       
        int compareHigh = node.Value.CompareTo(end);

        if (compareLow > 0)
        {
            this.Range(node.Left, result, start, end);
        }
        if (compareLow >= 0 && compareHigh <= 0)
        {
            result.Add(node.Value);
        }
        if (compareHigh < 0)
        {
            this.Range(node.Right, result, start, end);
        }


    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    private void EachInOrder(Node node, Action<T> action)
    {

        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);

    }

    private class Node
    {
        
        public T Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(T value)
        {
            this.Value = value;
            this.Left = null;
            this.Right = null;
        }

    }

}

public class Launcher
{
    public static void Main(string[] args)
    {
        
        BinarySearchTree<int> BST = new BinarySearchTree<int>();
        BST.Insert(5);
        BST.Insert(3);
        BST.Insert(4);
        BST.Insert(7);
        BST.Insert(6);

        foreach (int num in BST.Range(3, 7))
        {
            Console.WriteLine(num);
        }

    }
}
