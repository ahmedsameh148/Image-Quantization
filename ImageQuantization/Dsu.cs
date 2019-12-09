using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ImageQuantization
{
    class DSU
    {
        public int[] p;
        public int[] rank;
        
        public DSU()
        {
            p = new int[100005];
            rank = new int[100005];
        }

        public int FindParent(int node)
        {
            if (p[node] == node)
                return node;
            return p[node] = FindParent(p[node]);
        }

        public void connect(int node1, int node2)
        {
            int p1 = FindParent(node1), p2 = FindParent(node2);
            if (rank[p1] >= rank[p2])
            {
                p[p2] = p1;
                rank[p1] += rank[p2];
            }
            else
            {
                p[p1] = p2;
                rank[p2] += rank[p1];
            }
        }

        public void init()
        {
            for (int i = 0; i < 10005; i++)
            {
                rank[i] = 1;
                p[i] = i;
            }
        }
    }
}