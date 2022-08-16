using System;
using System.Collections.Generic;
using System.Text;

namespace N_puzzle
{
    class Node
    {
        public int g;
        public int h;
        public int M;

        public int cost;
        public Node parent;
        public int[,] state;
        public char come_from;

        //O(N^2) 
        public Node(int[,] puzzle, Node parent, int g, char came_from, int choice)
        {
            this.g = g;
            if (choice == 1)
            {
                this.h = calculateHamming(puzzle);
                this.cost = this.h + this.g;
            }
            else if (choice == 2)
            {
                this.M = calculateMenhatten(puzzle);
                this.cost = this.M + this.g;
            }
            this.parent = parent;
            this.state = puzzle;
            this.come_from = came_from;
        }

        //O(N^2)
        public void print_state(Node node, int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(node.state[i, j]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine(node.g);
            Console.WriteLine();
            if (node.state.GetLength(0) == 3)
                if (node.parent != null)
                    print_state(node.parent, size);
                else
                    Console.Write("");
        }

        //O(N^2)
        static int calculateHamming(int[,] puzzle)
        {
            int hamming = 0;
            int c = 1;
            int len = puzzle.GetLength(0);
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    if ((i + j + c) == puzzle[i, j] || puzzle[i, j] == 0) { }
                    else { hamming = hamming + 1; }
                }
                c = c + len - 1;
            }
            return hamming;
        }

        //O(N^2)
        static int calculateMenhatten(int[,] puzzle)
        {
            int DistanceSum = 0;

            int size = puzzle.GetLength(0);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (puzzle[i, j] != 0)
                    {
                        int x = (puzzle[i, j] - 1) / size;
                        int y = (puzzle[i, j] - 1) % size;
                        int dx;
                        if (x > i)
                            dx = x - i;
                        else
                            dx = i - x;
                        int dy;
                        if (y > j)
                            dy = y - j;
                        else
                            dy = j - y;
                        DistanceSum += dx + dy;
                    }
                }
            }
            return DistanceSum;
        }
    }
}
