using System;
using System.IO;
using System.Diagnostics;


namespace N_puzzle
{
    class Program
    {
        static bool goal(int[,] puzzle, int size)
        {
            int goal = 1;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (puzzle[i,j] == 0) continue;
                    else if (puzzle[i,j] != goal++) return false;
                }
            }
            return true;
        }
        static int ASTAR(int[,] puzzle, int size)
        {
            int g = 0, h = 0;
            PriorityQueue pq = new PriorityQueue();
            //pq.Enqueue(calculateMenhatten(puzzle,size));
            if (goal(puzzle, size))
            {
                //Console.WriteLine("\nSolved in %d moves", g);
            }
            return 0;
        }

        static int calculateMenhatten(int[,] puzzle,int size)
        {
            int DistanceSum = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (puzzle[i,j] != 0)
                    {
                        int x = (puzzle[i,j] - 1) / size;
                        int y = (puzzle[i,j] - 1) % size;
                        int dx = x - i;
                        int dy = y - j; 
                        DistanceSum += Math.Abs(dx) + Math.Abs(dy);
                        //total_distance += ABS(x, i) + ABS(y, j);
                    }
                }
            }
            return DistanceSum;
        }
        static int calculateHamming(int[] puzzle)
        {
            int hamming = 0;
            for(int i = 0; i < puzzle.Length-1; i++)
            {
                if(i+1 == puzzle[i] && puzzle[i]!=0)
                {

                }
                else
                {
                    hamming = hamming + 1;
                }
            }
            return hamming;
        }
        static bool isSolvable(int[] puzzle,int size,int index)
        {
            int inversions = 0;
            for (int i = 0; i < size*size - 1; i++)
                for (int j = i + 1; j < size*size; j++) {
                    if (puzzle[i] != 0 && puzzle[j]!=0 && puzzle[i] > puzzle[j])
                    {
                        //Console.WriteLine(puzzle[i]);
                        //Console.WriteLine(puzzle[j]);
                        inversions++;
                    }
                }
            if(size%2==0 && inversions%2==0 && (size-index) % 2 == 0)
            {
                return false;
            }else if(size % 2 == 0 && inversions % 2 != 0 && (size - index) % 2 == 0)
            {
                return true;
            }
            else if (size % 2 == 0 && inversions % 2 == 0 && (size - index) % 2 != 0)
            {
                return true;
            }
            Console.WriteLine(inversions);
            return (inversions % 2 == 0);
        }


        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();

            //Read File From BIN 
            FileStream file = new FileStream("TEST.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(file);
            string line = sr.ReadLine();

            //N (Number of Puzzles)
            int n = int.Parse(line);

            //Make Puzzel N x N 
            int[,] puzzle = new int[n, n];

            line = sr.ReadLine();
            //Handling Test Cases That has "Enter Space"
            if (line != "")
            {

            }
            else
            {
                line = sr.ReadLine();
            }

            //Blank (Saving Blank Position)
            int zeroIndex = 0;

            //Converting puzzel to 1D
            int[] puzzle1DArr = new int[n * n];
            int puzzle1DCounter = 0;

            //Read Puzzle from file (line)
            for (int i = 0; i < n; i++)
            {
                string[] vertices = line.Split(' ');

                for (int j = 0; j < n; j++)
                {
                    puzzle[i, j] = Int32.Parse(vertices[j]);

                    //Add it to 1D array
                    puzzle1DArr[puzzle1DCounter++] = puzzle[i, j];

                    //Blank Founded
                    if (puzzle[i, j] == 0)
                    {
                        zeroIndex = i;
                    }
                }
                line = sr.ReadLine();
            }


            if (isSolvable(puzzle1DArr, n, zeroIndex))
            {
                stopwatch.Start();

                Console.WriteLine("Solvable");
                //initial_state(puzzle, n);

                stopwatch.Stop();
                Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
                Console.WriteLine("Elapsed Time is {0} s", stopwatch.Elapsed.Seconds);
            }
            else
                Console.WriteLine("Not Solvable");

        }
    }
}
