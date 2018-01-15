using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {

            int size = int.Parse(Console.ReadLine());

            int[,] matrix = new int[size, size];

            int startRow = 0;
            int startCol = 0;

            int startValue = -2;
            int blockValue = -1;
            for (int i = 0; i < size; i++)
            {

                char[] row = Console.ReadLine().ToCharArray();

                for (int col = 0; col < size; col++)
                {
                    if (row[col] == '*')
                    {
                        startRow = i;
                        startCol = col;
                        matrix[i, col] = startValue;
                    }
                    else
                    {
                        matrix[i, col] = row[col] == 'x' ? blockValue : 0;
                    }
                    
                }

            }

            //Main
            Queue<Cell> queue = new Queue<Cell>();
            queue.Enqueue(new Cell(startRow, startCol));
            matrix[startRow, startCol] = 0;

            while (queue.Count > 0)
            {

                Cell current = queue.Dequeue();

                if (current.Row+1 < size 
                    && matrix[current.Row+1, current.Col] == 0)
                {
                    queue.Enqueue(new Cell(current.Row + 1, current.Col));
                    matrix[current.Row + 1, current.Col] += 
                        matrix[current.Row, current.Col] + 1;
                }
                if (current.Row - 1 >= 0
                    && matrix[current.Row - 1, current.Col] == 0)
                {
                    queue.Enqueue(new Cell(current.Row - 1, current.Col));
                    matrix[current.Row - 1, current.Col] +=
                        matrix[current.Row, current.Col] + 1;
                }
                if (current.Col + 1 < size
                    && matrix[current.Row, current.Col + 1] == 0)
                {
                    queue.Enqueue(new Cell(current.Row, current.Col + 1));
                    matrix[current.Row, current.Col + 1] +=
                        matrix[current.Row, current.Col] + 1;
                }
                if (current.Col - 1 >= 0
                    && matrix[current.Row, current.Col - 1] == 0)
                {
                    queue.Enqueue(new Cell(current.Row, current.Col - 1));
                    matrix[current.Row, current.Col - 1] +=
                        matrix[current.Row, current.Col] + 1;
                }

            }
            matrix[startRow, startCol] = startValue;

            //Print Matrix

            for (int i = 0; i < size; i++)
            {

                for (int j = 0; j < size; j++)
                {

                    int value = matrix[i, j];
                    if (value == blockValue)
                    {
                        Console.Write('x');
                    }
                    else if (value == startValue)
                    {
                        Console.Write('*');
                    }
                    else if (value == 0)
                    {
                        Console.Write('u');
                    }
                    else
                    {
                        Console.Write(value);
                    }

                }
                Console.WriteLine();
            }

        }
    }

    class Cell
    {

        public int Col { get; set; }
        public int Row { get; set; }
        public bool Visited { get; set; }
        

        public Cell(int row, int col, bool visited = false)
        {
            this.Row = row;
            this.Col = col;
        }

    }
}
