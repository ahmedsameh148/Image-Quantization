using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class Prim
    {
        //Calculate the Power in O(log2(Power))
        public long Power(long Base, long power)
        {
            if (power == 1) return Base;
            if (power == 0) return 1;
            long res = Power(Base, power / 2);
            if (power % 2 == 1) return Base * res * res;
            return res * res;
        }

        //Calculate The Distance Between Any 2 Colors
        public double calculateDistance(RGBPixel color1, RGBPixel color2)
        {
            return Math.Sqrt(Power(Math.Abs(color1.red - color2.red), 2) + Power(Math.Abs(color1.green - color2.green), 2) + Power(color1.blue - color2.blue, 2));
        }

        //Calculate The Minimum Spanning Tree 
        public double getMst(int x, ref List<KeyValuePair<KeyValuePair<int, int>, double>> edges, int D, List<RGBPixel> Colors)
        {
            //Priority Queue To Get The Minimum Distance Between Any 2 Nodes
            PriorityQueue<PriorityQueueItem> Q = new PriorityQueue<PriorityQueueItem>();
            //Save The Total Weight Of The Minimum Spanning Tree
            double minimumCost = 0;
            //Save The Weight Of Each Node
            double[] weights = new double[D + 1];
            //Save The Parent Of Each Node
            int[] parent = new int[D + 1];
            //Initialize The Weight Of Each Node With 10^9
            for (int i = 1; i <= D; i++) { weights[i] = 1000000000; }
            //Push The Start Node In The Priority Queue
            Q.Enqueue(new PriorityQueueItem(0, x));
            //Array To Know The Visited Nodes
            bool[] marked = new bool[D + 1];

            while (Q.Count > 0)
            {
                // Select the edge with minimum weight
                PriorityQueueItem cur = Q.Dequeue();
                // Checking for cycle
                if (marked[cur.Value] == true)
                    continue;
                //Adding The Weight Of  The Edge To The Total Weight Of Th MST
                minimumCost += cur.Key;
                //Mark The Vertix As Visited
                marked[cur.Value] = true;
                //Adding The Edge To The Resulting Tree
                edges.Add(new KeyValuePair<KeyValuePair<int, int>, double>(new KeyValuePair<int, int>(parent[cur.Value], cur.Value), weights[cur.Value]));
                //Looping Among All Childs Of The Current Vertix
                for (int i = 0; i < D; ++i)
                {
                    //If added will cause cycle
                    if (marked[i] == false)
                    {
                        //Calculate The Weight On The Edge Between The Current Vertix And His Child
                        double weight = calculateDistance(Colors[cur.Value], Colors[i]);
                        //Check If I Pushed The Same Vertix With A Smaller Cost
                        if (weights[i] > weight)
                        {
                            //Updating The Weight Of The Vertix
                            weights[i] = weight; parent[i] = cur.Value;
                            //Adding The Vertix To The Queue
                            Q.Enqueue(new PriorityQueueItem(weight, i));
                        }
                    }
                }
            }
            //Return The Total Weight Of The MST
            return minimumCost;
        }
    }
}
