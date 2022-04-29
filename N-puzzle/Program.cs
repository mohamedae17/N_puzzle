using System;
using System.IO;

namespace N_puzzle
{
    class Program
    {
        static bool isSolvable(int[,] puzzle)
        {
            int inversions = 0;
            for (int i = 0; i < 3 - 1; i++)
                for (int j = i + 1; j < 3; j++)
                    if (puzzle[j, i] > 0 && puzzle[j, i] > puzzle[i, j])
                    {
                        inversions++;
                    }
            return (inversions % 2 == 0);
        }

        static void Main(string[] args)
        {
            FileStream file;
            int n;
            StreamReader sr;
            string line;
            file = new FileStream("8 Puzzle (1).txt", FileMode.Open, FileAccess.Read);
            string[,] puzzle;
            sr = new StreamReader(file);
            line = sr.ReadLine();
            n = int.Parse(line);
            puzzle = new string[n, n];
            line = sr.ReadLine();

            for (int i = 0; i < n; i++)
            {
                line = sr.ReadLine();
                string[] vertices = line.Split(' ');
                for (int j = 0; j < n; j++)
                {
                    puzzle[i, j] = vertices[j];
                }
            }
            int[,] puz = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    puz[i, j] = Int32.Parse(puzzle[i, j]);
                }
            }
            if (isSolvable(puz))
                Console.WriteLine("Solvable");
            else
                Console.WriteLine("Not Solvable");


        }
    }
}
