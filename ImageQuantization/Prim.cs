using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class Prim
    {
        public Prim()
        {

        }

        public double Square(double n)
        {
            double mid = 0, mul = n + 1, i = 0, j = n;

            while (true)
            {
                mid = (i + j) / 2;
                mul = mid * mid;
                if ((mul == n) || (Math.Abs(mul - n) < 0.000001))
                {
                    int temp = (int)mid;
                    if (temp * temp == n) return temp;
                    return mid;
                }
                else if (mul < n)
                    i = mid;
                else
                    j = mid;
            }
        }

        public long Power(long Base, long power)
        {
            if (power == 1) return Base;
            if (power == 0) return 1;
            long res = Power(Base, power / 2);
            if (power % 2 == 1) return Base * res * res;
            return res * res;
        }

        public double calculateDistance(RGBPixel color1, RGBPixel color2)
        {
            return Square(Power(Math.Abs(color1.red - color2.red), 2) + Power(Math.Abs(color1.green - color2.green), 2) + Power(color1.blue - color2.blue, 2));
        }

        public double getMst(int x, ref List<KeyValuePair<KeyValuePair<int, int>, double>> edges, int D, List<RGBPixel> Colors)
        {
            PriorityQueue<PriorityQueueItem> Q = new PriorityQueue<PriorityQueueItem>();

            double minimumCost = 0;
            double[] weights = new double[D + 1];
            int[] parent = new int[D + 1];
            for (int i = 0; i <= D; i++) { weights[i] = 1000000000; }
            Q.Enqueue(new PriorityQueueItem(0, x));
            bool[] marked = new bool[D + 1];
            while (Q.Count > 0)
            {
                // Select the edge with minimum weight
                PriorityQueueItem cur = Q.Dequeue();
                // Checking for cycle
                if (marked[cur.Value] == true)
                    continue;

                minimumCost += cur.Key;
                marked[cur.Value] = true;
                edges.Add(new KeyValuePair<KeyValuePair<int, int>, double>(new KeyValuePair<int, int>(parent[cur.Value], cur.Value), weights[cur.Value]));
                for (int i = 0; i < D; ++i)
                {
                    //If added will cause cycle
                    if (marked[i] == false)
                    {
                        double weight = calculateDistance(Colors[cur.Value], Colors[i]);
                        if (weights[i] > weight)
                        {
                            weights[i] = weight; parent[i] = cur.Value;
                            Q.Enqueue(new PriorityQueueItem(weight, i));
                        }
                    }
                }
            }
            return minimumCost;
        }
    }
}
