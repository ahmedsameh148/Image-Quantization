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
            // theta(1) per call
            dsu.init();
        }

        
        //takes  O(E logV)
        public double ComputeMSTPath(double[,] matrix, int length, ref double[,] resGraph,ref List<KeyValuePair<KeyValuePair<int, int>, double>> res)
        {
           
             res = new List<KeyValuePair<KeyValuePair<int, int>, double>>();
            List<KeyValuePair<KeyValuePair<int, int>, double>> edges = new List<KeyValuePair<KeyValuePair<int, int>, double>>();
           // O(lenght^2)
            for (int i = 0; i < length; i++)
            {
                for (int y = i; y < length; y++)
                {
                    //theta(1)
                    KeyValuePair<int, int> t = new KeyValuePair<int, int>(i, y);
                    //theta(1)
                    KeyValuePair<KeyValuePair<int, int>, double> tmp = new KeyValuePair<KeyValuePair<int, int>, double>(t, matrix[i, y]);
                    //theta(n) to insert in list
                    edges.Add(tmp);
                }
            }

            // take theta(E log V) but in worst case take O(V^2)
            edges = edges.OrderBy(x => x.Value).ToList();

            //theta(1)
            double treeSum = 0;

            // O(edges count)
            for (int i = 0; i < edges.Count(); i++)
            {
                KeyValuePair<int, int> r = new KeyValuePair<int, int>(edges[i].Key.Key, edges[i].Key.Value);
                KeyValuePair<KeyValuePair<int, int>, double> r1 = new KeyValuePair<KeyValuePair<int, int>, double>(r, edges[i].Value);
                // O(1)
                if (dsu.FindParent(edges[i].Key.Key) != dsu.FindParent(edges[i].Key.Value))
                {
                    //O(n)
                    res.Add(r1);
                    //theta(1)
                    dsu.connect(edges[i].Key.Key, edges[i].Key.Value);
                    //theta(1)
                    treeSum += edges[i].Value;
                }
            }

            //O(res count)
            for (int i = 0; i < res.Count; i++)
            {
                //theta(1)
                resGraph[res[i].Key.Key, res[i].Key.Value] = res[i].Value;
            }
            //theta(1)
            return treeSum;
        }

       
    }
}
