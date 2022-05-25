using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;


namespace N_puzzle
{
    class Program
    {
        static int choice;
        static PriorityQueue pQ = new PriorityQueue();
        static HashSet<int> myhash1 = new HashSet<int>();
        public static int level = 0;
        public static int[,] goal;

        //O(S^2)
        static bool isSolvable(int[] puzzle, int n, int index)
        {
            //If Size Odd & Inversion is even then Solvable 
            int inversions = 0;

            //Check Inverstions -> H.M Inversions in the Puzzle ?
            for (int i = 0; i < n * n - 1; i++)
                for (int j = i + 1; j < n * n; j++)
                {
                    if (puzzle[i] != 0 && puzzle[j] != 0 && puzzle[i] > puzzle[j])
                    {
                        inversions++;
                    }
                }

            //Size is Even

            //Inversion is Even & Blank Even from Buttom Posistion
            if (n % 2 == 0 && inversions % 2 == 0 && (n - index) % 2 == 0)
            {
                //Not Solvable
                return false;
            }

            //Inversion is Odd & Blank is even from bottom Position
            else if (n % 2 == 0 && inversions % 2 != 0 && (n - index) % 2 == 0)
            {
                //Solvable 
                return true;
            }

            //Inversion is Even & Blank Size from bottom Positions is ODD
            else if (n % 2 == 0 && inversions % 2 == 0 && (n - index) % 2 != 0)
            {
                //Solvable
                return true;
            }

            //Size is ODD

            //IF Inversion is ODD return False 
            //IF Inversion is Even return True
            return (inversions % 2 == 0);
        }

        //O(S) O(N^2)
        static int hashGoal;
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

            //O(S) O(N^2)
            hashGoal = hashing(goal);

            //Root Node 
            Node intial = new Node(puzzle, null, level, 'R', choice);

            //O(S) O(N^2)
            myhash1.Add(hashing(intial.state));

            //O(Log(N)
            pQ.Enqueue(intial);

            //ELog(V)
            AStar(intial, n);
        }

        //O(N^2) O(S)
        public static int hashing(int[,] array)
        {
            int hash = 1;
            int arlenght = array.GetLength(0);

            for (int i = 0; i < arlenght; i++)
            {
                for (int j = 0; j < arlenght; j++)
                {
                    hash = hash * 5 + array[i, j];
                }
            }
            return hash;
        }

        //ELOG(V)
        public static void AStar(Node puzzle, int n)
        {
            //Looping on PQ till break 
            while (pQ.Count != 0)
            {
                //Take least cost state
                puzzle = pQ.Dequeue();

                //Search for Blank State in the puzzle
                //O(S)
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

                //O(S)
                //Copying Puzzle -> Left / Right / Down / Up 
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

                //O(1)
                //if zero founded upward 
                if (yup >= 0 && yup < n)
                {
                    uppuzzle[indexx, indexy] = puzzle.state[yup, indexy];
                    uppuzzle[yup, indexy] = puzzle.state[indexx, indexy];
                }
                //if zero dounded downward
                if (ydown >= 0 && ydown < n)
                {
                    downpuzzle[indexx, indexy] = puzzle.state[ydown, indexy];
                    downpuzzle[ydown, indexy] = puzzle.state[indexx, indexy];
                }
                //if zero founded right
                if (xright >= 0 && xright < n)
                {
                    rightpuzzle[indexx, indexy] = puzzle.state[indexx, xright];
                    rightpuzzle[indexx, xright] = puzzle.state[indexx, indexy];
                }
                //if zero founded left
                if (xleft >= 0 && xleft < n)
                {
                    leftpuzzle[indexx, indexy] = puzzle.state[indexx, xleft];
                    leftpuzzle[indexx, xleft] = puzzle.state[indexx, indexy];
                }

                level++;

                //O(S)
                Node uppuzzle1 = new Node(uppuzzle, puzzle, level, 'u', choice);
                Node downpuzzle1 = new Node(downpuzzle, puzzle, level, 'd', choice);
                Node rightpuzzle1 = new Node(rightpuzzle, puzzle, level, 'r', choice);
                Node leftpuzzle1 = new Node(leftpuzzle, puzzle, level, 'l', choice);

                //O(1)
                if (ydown < n && !myhash1.Contains(hashing(downpuzzle)))
                {
                    pQ.Enqueue(downpuzzle1);
                }
                if (yup >= 0 && !myhash1.Contains(hashing(uppuzzle)))
                {
                    pQ.Enqueue(uppuzzle1);
                }
                if (xleft >= 0 && !myhash1.Contains(hashing(leftpuzzle)))
                {
                    pQ.Enqueue(leftpuzzle1);
                }
                if (xright < n && !myhash1.Contains(hashing(rightpuzzle)))
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
                    //Extracting min from the opened list should be less than O(N) (e.g. logarithmic or constant time) O(1)
                    level = pQ.Peek().g;
                    myhash1.Add(hashing(pQ.Peek().state));
                }
            }
        }

        //O(N^2) O(S)
        static bool isEndState(int[,] puzzle, int n)
        {
            if (hashGoal == hashing(puzzle))
            {

                return true;
            }
            else
            {
                return false;
            }

        }

        static Stopwatch stopwatch = new Stopwatch();
        static FileStream file = new FileStream("8 Puzzle (3).txt", FileMode.Open, FileAccess.Read);
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
            //O(S)
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
                choice = int.Parse(distance);

                stopwatch.Start();
                if (choice == 1 || choice == 2)
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
