using System;
using System.IO;

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
            // NOW WWWHHHHAAAAAAATTTT?????
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
            FileStream file;
            int n;
            StreamReader sr;
            string line;
            file = new FileStream("8 Puzzle (3).txt", FileMode.Open, FileAccess.Read);
            string[,] puzzle;
            sr = new StreamReader(file);
            line = sr.ReadLine();
            n = int.Parse(line);
            puzzle = new string[n, n];

            //line = sr.ReadLine();
            line = sr.ReadLine();
            if (line != "")
            {

            }
            else
            {
                line = sr.ReadLine();
            }
            int zeroIndex=0;
            int zeroJndex = 0;


            for (int i = 0; i < n; i++)
            {    
                string[] vertices = line.Split(' ');
                for (int j = 0; j < n; j++)
                {
                    puzzle[i, j] = vertices[j];
                }
                line = sr.ReadLine();
            }
            int[,] puz2d = new int[n, n];
            int[] puz1d = new int[n * n];
            int k = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    puz2d[i, j] = Int32.Parse(puzzle[i, j]);
                    if (puz2d[i, j] == 0)
                    {
                        zeroIndex = i;
                        zeroJndex = j;
                    }
                    puz1d[k++] = Int32.Parse(puzzle[i, j]);
                }
            }
            if (isSolvable(puz1d,n,zeroIndex))
                Console.WriteLine("Solvable");
            else
                Console.WriteLine("Not Solvable");

            int Hamming = calculateHamming(puz1d);
            Console.WriteLine(Hamming);

            int Menhatten = calculateMenhatten(puz2d,n);
            Console.WriteLine(Menhatten);
            ////////////////////////////////////////////////////
            //PriorityQueue pq = new PriorityQueue();
            //pq.Enqueue(10);
            //pq.Enqueue(2);
            //pq.Enqueue(5);
            //pq.Enqueue(6);

            //Console.WriteLine(pq.Peek());
            //pq.Dequeue();
            //Console.WriteLine(pq.Peek());
        }
    }
}
