using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class buildGraph
    {
        private RGBPixel[,] ImageMatrix;
        public buildGraph(RGBPixel[,] Image)
        {
            ImageMatrix = Image;
        }
        public List<RGBPixel> distinct_Colors()
        {
            int Dcolors_width = ImageOperations.GetWidth(ImageMatrix);
            int Dcolors_height = ImageOperations.GetHeight(ImageMatrix);
            HashSet<RGBPixel> Distinct = new HashSet<RGBPixel>();
            for (int i = 0; i < Dcolors_height; i++)
            {
                for (int j = 0; j < Dcolors_width; j++)
                {
                    Distinct.Add(ImageMatrix[i, j]);
                }
            }

            List<RGBPixel> Distinctcolors = new List<RGBPixel>();
            foreach (RGBPixel color in Distinct)
            {
                Distinctcolors.Add(color);
            }

            return Distinctcolors;
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

        public double calculateDistance(RGBPixel color1, RGBPixel color2) {
            return Square(Power(Math.Abs(color1.red - color2.red), 2) + Power(Math.Abs(color1.green - color2.green), 2) + Power(color1.blue - color2.blue, 2));
        }

        public void generatePaths(List<RGBPixel> Colors, ref double[,] Matrix)
        {
            for (int i = 0; i < Colors.Count; i++)
            {
                for (int y = i; y < Colors.Count; y++)
                {
                    Matrix[i, y] = calculateDistance(Colors[i], Colors[y]);
                    Matrix[y, i] = calculateDistance(Colors[i], Colors[y]);
                }
            }
        }


        /*public static double[,] calculate_distance(RGBPixel[,] ImageMatrix)
        {

            List<RGBPixel> l = new List<RGBPixel>();
            l = getColors(ImageMatrix);
            double res;
            List<info_edge> graph[] = new List<info_edge>();
            // double[,] graph = new double[l.Count, l.Count];

            /* for (int i=0;i<l.Count;i++)
             {
                 for (int j=0;j<l.Count;j++)
                 {
                     if (i == j) graph[i, j] = 0;
                     if (j>i)
                     {
                         res = power(l[i].red - l[j].red, 2) + power(l[i].green - l[j].green, 2) + power(l[i].blue - l[j].blue, 2);
                         res = Square(res);
                         graph[i, j] = res;
                         graph[j, i] = res;
                     }
                 }

             }
             return graph;
            info_edge e = new info_edge();

            for (int i = 0; i < l.Count; i++)
            {
                e.start = e.end = l[i];
                e.value = 0;
                graph.Add(e);
                for (int j = i + 1; j < l.Count; j++)
                {
                    e.value = power(l[i].red - l[j].red, 2) + power(l[i].green - l[j].green, 2) + power(l[i].blue - l[j].blue, 2);
                    e.value = Square(e.value);
                    e.start = l[i];
                    e.end = l[j];
                    graph.Add(e);
                    e.start = l[j];
                    e.end = l[i];
                }
            }


        }*/

    }
}
