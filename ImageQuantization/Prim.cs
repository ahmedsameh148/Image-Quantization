using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class Prim
    {
       

        //Calculate The Distance Between Any 2 Colors
        public double calculateDistance(RGBPixel color1, RGBPixel color2)
        {
            return Math.Sqrt( ((color1.red - color2.red)* (color1.red - color2.red)) + ((color1.green - color2.green)*(color1.green - color2.green)) + ((color1.blue - color2.blue)* (color1.blue - color2.blue)));
        }

        //Calculate The Minimum Spanning Tree 
        public double getMst(ref List<KeyValuePair<KeyValuePair<int, int>, double>> edges, int D, List<RGBPixel> Colors)
        {
            //Priority Queue To Get The Minimum Distance Between Any 2 Nodes
           // PriorityQueue<PriorityQueueItem> Q = new PriorityQueue<PriorityQueueItem>();
            //Save The Total Weight Of The Minimum Spanning Tree
            double minimumCost = 0;
            //Save The Weight Of Each Node
            double[] weights = new double[D + 1];
            //Save The Parent Of Each Node
            int[] parent = new int[D + 1];
            //Initialize The Weight Of Each Node With 10^9
            for (int i = 1; i <= D; i++) { weights[i] = 1000000000; }
            //Push The Start Node In The Priority Queue
            int cur = 1;
            double mn;
            //Array To Know The Visited Nodes
            bool[] marked = new bool[D + 1];
            weights[1] = 0;
            while (cur<D)
            {
                marked[cur] = true;
                mn = 1000000000;
                int child = 0;
                minimumCost += weights[cur];
                for (int i = 1; i < D; ++i)
                {
                    //If added will cause cycle
                    if (marked[i] == false)
                    {
                        //Calculate The Weight On The Edge Between The Current Vertix And His Child
                        double weight = calculateDistance(Colors[cur], Colors[i]);
                        //double weight = 0;
                        //Check If I Pushed The Same Vertix With A Smaller Cost
                        if (weights[i] > weight)
                        {
                            //Updating The Weight Of The Vertix
                            weights[i] = weight; parent[i] = cur;
                            //Adding The Vertix To The Queue
                        }
                        if (weights[i] < mn)
                        {
                            mn = weights[i];
                            child = i;
                        }
                    }
                }
                if (child == 0) break;
                edges.Add(new KeyValuePair<KeyValuePair<int, int>, double>(new KeyValuePair<int, int>(cur, child), mn));
                cur = child;
            }
            
            //Return The Total Weight Of The MST
            return minimumCost;
        }
    }
}
