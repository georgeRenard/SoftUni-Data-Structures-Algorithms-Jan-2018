using System;
using System.Collections.Generic;

public class AStar
{

    private char[,] maze;

    public AStar(char[,] map)
    {
        this.maze = map;
    }

    public static int GetH(Node current, Node goal)
    {
        int deltaRow = Math.Abs(current.Row - goal.Row);
        int deltaCol = Math.Abs(current.Col - goal.Col);

        return deltaRow + deltaCol;
    }

    public IEnumerable<Node> GetPath(Node start, Node goal)
    {

        PriorityQueue<Node> pQue = new PriorityQueue<Node>();
        Dictionary<Node, Node> parents = new Dictionary<Node, Node>();
        // Distance between start and current
        Dictionary<Node, int> gCost = new Dictionary<Node, int>();

        gCost.Add(start, 0);
        parents.Add(start, null);
        pQue.Enqueue(start);

        while (pQue.Count > 0)
        {
            //Get node with lowest FCost or |(Start node)|
            Node current = pQue.Dequeue();

            if (current.Equals(goal))
            {
                break;
            }

            List<Node> nodesNear = this.GetNearbyNodes(current);
            int newCost = gCost[current] + 1;

            foreach (var node in nodesNear)
            {
                if (!gCost.ContainsKey(node) || newCost < gCost[node])
                {
                    node.F = newCost + GetH(node, goal);
                    parents[node] = current;
                    gCost[node] = newCost;
                    pQue.Enqueue(node);
                }
            }

        }
        return this.ReconstructPath(parents, start, goal);
    }

    private IEnumerable<Node> ReconstructPath(Dictionary<Node, Node> parents, Node start, Node goal)
    {

        if (!parents.ContainsKey(goal))
        {
           return new List<Node>()
           {
               start
           };
        }

        Node current = parents[goal];
        Stack<Node> path = new Stack<Node>();
        path.Push(goal);
        while (!current.Equals(start))
        {
            path.Push(current);
            current = parents[current];
        }
        path.Push(start);
        return path;
    }

    private List<Node> GetNearbyNodes(Node current)
    {
        int row = current.Row;
        int col = current.Col;
        
        int rowUp = row - 1;
        int rowDown = row + 1;
        int colLeft = col - 1;
        int colRight = col + 1;

        List<Node> result = new List<Node>();
        this.AddToQueue(result, rowUp, col);
        this.AddToQueue(result, rowDown, col);
        this.AddToQueue(result, row, colLeft);
        this.AddToQueue(result, row, colRight);

        return result;
    }

    private void AddToQueue(List<Node> list, int row, int col)
    {
        if (IsInBounds(row, col) && !IsWall(row, col))
        {
            Node newNode = new Node(row, col);
            list.Add(newNode);
        }
    }

    private bool IsInBounds(int row, int col)
    {
        return (row >= 0 && row < this.maze.GetLength(0))
            && (col >= 0 && col < this.maze.GetLength(1)) ;
    }

    private bool IsWall(int row, int col)
    {
        return this.maze[row, col] == 'W';
    }
}

