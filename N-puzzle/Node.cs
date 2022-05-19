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


        public Node(int[,] puzzle, Node parent, int g, char came_from)
        {
            this.g = g;
            this.h = calculateHamming(puzzle);
            this.M = calculateMenhatten(puzzle);
            this.cost = this.M + this.g;
            //this.cost = this.h + this.g;
            this.parent = parent;
            this.state = puzzle;
            this.come_from = came_from;
        }
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
        }
        static int calculateHamming(int[,] puzzle)
        {
            int hamming = 0;
            int c = 1;
            for (int i = 0; i < puzzle.GetLength(0); i++)
            {
                for (int j = 0; j < puzzle.GetLength(0); j++)
                {
                    if ((i + j + c) == puzzle[i, j] || puzzle[i, j] == 0) { }
                    else { hamming = hamming + 1; }
                }
                c = c + puzzle.GetLength(0) - 1;
            }
            return hamming;
        }
        static int calculateMenhatten(int[,] puzzle)
        {
            int DistanceSum = 0;
            for (int i = 0; i < puzzle.GetLength(0); i++)
            {
                for (int j = 0; j < puzzle.GetLength(0); j++)
                {
                    if (puzzle[i, j] != 0)
                    {
                        int x = (puzzle[i, j] - 1) / puzzle.GetLength(0);
                        int y = (puzzle[i, j] - 1) % puzzle.GetLength(0);
                        int dx;
                        if (x > -i)
                            dx = x - i;
                        else
                            dx = i - x;
                        int dy;
                        if (y > j)
                            dy = y - j;
                        else
                            dy = j - y;
                        DistanceSum += Math.Abs(dx) + Math.Abs(dy);
                    }
                }
            }
            return DistanceSum;
        }
    }
}
