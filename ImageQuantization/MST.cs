using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class MST
    {
        DSU dsu;
        public MST()
        {
            dsu = new DSU();
            dsu.init();
        }

        public double ComputeMSTPath(double[,] matrix, int length, ref double[,] resGraph)
        {
            List<KeyValuePair<KeyValuePair<int, int>, double>> res = new List<KeyValuePair<KeyValuePair<int, int>, double>>();
            List<KeyValuePair<KeyValuePair<int, int>, double>> edges = new List<KeyValuePair<KeyValuePair<int, int>, double>>();
            for (int i = 0; i < length; i++)
            {
                for (int y = i; y < length; y++)
                {
                    KeyValuePair<int, int> t = new KeyValuePair<int, int>(i, y);
                    KeyValuePair<KeyValuePair<int, int>, double> tmp = new KeyValuePair<KeyValuePair<int, int>, double>(t, matrix[i, y]);
                    edges.Add(tmp);
                }
            }

            edges = edges.OrderBy(x => x.Value).ToList();
            double treeSum = 0;
            for (int i = 0; i < edges.Count(); i++)
            {
                KeyValuePair<int, int> r = new KeyValuePair<int, int>(edges[i].Key.Key, edges[i].Key.Value);
                KeyValuePair<KeyValuePair<int, int>, double> r1 = new KeyValuePair<KeyValuePair<int, int>, double>(r, edges[i].Value);

                if (dsu.FindParent(edges[i].Key.Key) != dsu.FindParent(edges[i].Key.Value))
                {
                    res.Add(r1);
                    dsu.connect(edges[i].Key.Key, edges[i].Key.Value);
                    treeSum += edges[i].Value;
                }

            }
            for (int i = 0; i < res.Count; i++)
            {
                resGraph[res[i].Key.Key, res[i].Key.Value] = res[i].Value;
            }
            return treeSum;
        }

        public void printMST(List<KeyValuePair<KeyValuePair<int, int>, int>> resultMsT)
        {
            for (int i = 0; i < resultMsT.Count(); i++)
            {
                Console.WriteLine(resultMsT[i].Key.Key);
                Console.WriteLine(resultMsT[i].Key.Value);
                Console.WriteLine(resultMsT[i].Value);
            }
        }
    }
}
