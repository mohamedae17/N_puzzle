using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;


namespace N_puzzle
{
    class Program
    {
        static int a;
        static PriorityQueue pQ = new PriorityQueue();
        static HashSet<int> myhash1 = new HashSet<int>();
        public static int g = 0;
        public static int[,] goal;

        /////// 
        static bool isSolvable(int[] puzzle, int size, int index)
        {
            int inversions = 0;

            for (int i = 0; i < size * size - 1; i++)
                for (int j = i + 1; j < size * size; j++)
                {
                    if (puzzle[i] != 0 && puzzle[j] != 0 && puzzle[i] > puzzle[j])
                    {
                        inversions++;
                    }
                }

            if (size % 2 == 0 && inversions % 2 == 0 && (size - index) % 2 == 0)
            {
                return false;
            }

            else if (size % 2 == 0 && inversions % 2 != 0 && (size - index) % 2 == 0)
            {
                return true;
            }

            else if (size % 2 == 0 && inversions % 2 == 0 && (size - index) % 2 != 0)
            {
                return true;
            }

            return (inversions % 2 == 0);
        }

        static int x;
        public static void initial_state(int[,] puzzle, int n)
        {
            //Set Goal 
            goal = new int[n, n];

            //Puzzle Starting From 1
            int num = 1;

            for (int i = 0; i < n; i++)
            {
                //First Row
                for (int j = 0; j < n; j++)
                {
                    //Coulmns
                    goal[i, j] = num;
                    num++;
                }
            }

            //Last = 0 (Blank) Override number 9 in the loop
            goal[n - 1, n - 1] = 0;
            x = GG(goal);
            //Root Node 
            Node intial = new Node(puzzle, null, g, 'R', a);

            myhash1.Add(GG(intial.state));
            pQ.Enqueue(intial);
            swap(intial, n);
        }

        public static int GG(int[,] array)
        {
            int hash = 17;
            int arlenght = array.GetLength(0);
            for (int i = 0; i < arlenght; i++)
            {
                for (int j = 0; j < arlenght; j++)
                {
                    hash = hash * 31 + array[i, j];
                }
            }
            return hash;
        }
        public static void swap(Node puzzle, int n)
        {
            while (pQ.Count != 0)
            {
                puzzle = pQ.Dequeue();

                int indexx = -1, indexy = -1;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (puzzle.state[i, j] == 0)
                        {
                            indexx = i;
                            indexy = j;
                            break;
                        }

                    }
                    if (indexx != -1 && indexy != -1)
                    {
                        break;
                    }
                }
                int yup = indexx - 1;
                int ydown = indexx + 1;
                int xright = indexy + 1;
                int xleft = indexy - 1;
                int[,] leftpuzzle = new int[n, n];
                int[,] rightpuzzle = new int[n, n];
                int[,] downpuzzle = new int[n, n];
                int[,] uppuzzle = new int[n, n];

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        leftpuzzle[i, j] = puzzle.state[i, j];
                        rightpuzzle[i, j] = puzzle.state[i, j];
                        downpuzzle[i, j] = puzzle.state[i, j];
                        uppuzzle[i, j] = puzzle.state[i, j];

                    }
                }

                if (yup >= 0 && yup < n)
                {
                    uppuzzle[indexx, indexy] = puzzle.state[yup, indexy];
                    uppuzzle[yup, indexy] = puzzle.state[indexx, indexy];
                }
                if (ydown >= 0 && ydown < n)
                {
                    downpuzzle[indexx, indexy] = puzzle.state[ydown, indexy];
                    downpuzzle[ydown, indexy] = puzzle.state[indexx, indexy];
                }

                if (xright >= 0 && xright < n)
                {
                    rightpuzzle[indexx, indexy] = puzzle.state[indexx, xright];
                    rightpuzzle[indexx, xright] = puzzle.state[indexx, indexy];
                }
                if (xleft >= 0 && xleft < n)
                {
                    leftpuzzle[indexx, indexy] = puzzle.state[indexx, xleft];
                    leftpuzzle[indexx, xleft] = puzzle.state[indexx, indexy];
                }
                g++;
                Node uppuzzle1 = new Node(uppuzzle, puzzle, g, 'u', a);
                Node downpuzzle1 = new Node(downpuzzle, puzzle, g, 'd', a);
                Node rightpuzzle1 = new Node(rightpuzzle, puzzle, g, 'r', a);
                Node leftpuzzle1 = new Node(leftpuzzle, puzzle, g, 'l', a);

                if (ydown < n && !myhash1.Contains(GG(downpuzzle)))
                {
                    pQ.Enqueue(downpuzzle1);
                }
                if (yup >= 0 && !myhash1.Contains(GG(uppuzzle)))
                {
                    pQ.Enqueue(uppuzzle1);
                }
                if (xleft >= 0 && !myhash1.Contains(GG(leftpuzzle)))
                {
                    pQ.Enqueue(leftpuzzle1);
                }
                if (xright < n && !myhash1.Contains(GG(rightpuzzle)))
                {
                    pQ.Enqueue(rightpuzzle1);
                }

                if (isEndState(pQ.Peek().state, n))
                {
                    Node finall = pQ.Peek();
                    finall.print_state(finall, n);
                    Console.WriteLine(pQ.Peek().g);
                    break;
                }
                else
                {
                    g = pQ.Peek().g;
                    myhash1.Add(GG(pQ.Peek().state));
                }
            }
        }
        static bool isEndState(int[,] puzzle, int n)
        {

            //int i = 0, j = 0;
            //while (i != n && j != n)
            //{
            //    if (goal[i, j] != puzzle[i, j])
            //    {
            //        return false;
            //    }
            //    else if (j == n - 1)
            //    {
            //        j = 0;
            //        i++;
            //    }
            //    else
            //    {
            //        j++;
            //    }
            //}
            if (x == GG(puzzle))
            {

                return true;
            }
            else
            {
                return false;
            }
            //for (int i = 0; i < n; i++)
            //{
            //    for (int j = 0; j < n; j++)
            //    {
            //        if (goal[i, j] != puzzle[i, j]) return false;
            //    }
            //}
            //return true;
        }

        static Stopwatch stopwatch = new Stopwatch();
        static FileStream file = new FileStream("8 Puzzle (2).txt", FileMode.Open, FileAccess.Read);
        static StreamReader sr = new StreamReader(file);
        static void Main(string[] args)
        {

            //Read File From BIN 

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
                string[] fromfile = line.Split(' ');

                for (int j = 0; j < n; j++)
                {
                    puzzle[i, j] = Int32.Parse(fromfile[j]);

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

                Console.WriteLine("Solvable");
                Console.WriteLine("1.Hamming or 2.Manhattan ?");
                string distance = Console.ReadLine();
                a = int.Parse(distance);
                stopwatch.Start();
                if (a == 1)
                    initial_state(puzzle, n);
                else if (a == 2)
                    initial_state(puzzle, n);
                else
                    Console.WriteLine("wrong choose..");
                stopwatch.Stop();
                Console.WriteLine("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
                Console.WriteLine("Elapsed Time is {0} s", stopwatch.Elapsed.Seconds);
            }
            else
                Console.WriteLine("Not Solvable");

        }
    }
}
