using System;
using System.Collections.Generic;
using System.Text;

namespace N_puzzle
{
    class PriorityQueue
    {
        public List<Node> list;

        public int Count { get { return list.Count; } }

        public PriorityQueue()
        {
            list = new List<Node>();
        }

        public PriorityQueue(int count)
        {
            list = new List<Node>(count);
        }

        //LOG(N)
        public void Enqueue(Node x)
        {
            list.Add(x);
            int i = list.Count - 1;
            //Using Binary Search
            while (i > 0)
            {
                //Check the cost with the existing nodes
                int p = (i - 1) / 2;
                if (list[p].cost <= x.cost) break;

                list[i] = list[p];
                i = p;
            }

            if (list.Count > 0) list[i] = x;
        }

        //LOG(N)
        public Node Dequeue()
        {
            Node min = Peek();
            Node root = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);

            int i = 0;
            while (i * 2 + 1 < list.Count)
            {
                int a = i * 2 + 1;
                int b = i * 2 + 2;
                int c = b < list.Count && list[b].cost < list[a].cost ? b : a;

                if (list[c].cost >= root.cost) break;
                list[i] = list[c];
                i = c;
            }

            if (list.Count > 0) list[i] = root;
            return min;
        }

        //O(1)
        public Node Peek()
        {
            //Extracting min from the opened list should be less than O(N) (e.g. logarithmic or constant time) O(1)
            if (list.Count == 0) throw new InvalidOperationException("Queue is empty.");
            return list[0];
        }

        //O(1)
        public void Clear()
        {
            list.Clear();
        }
    }
}
