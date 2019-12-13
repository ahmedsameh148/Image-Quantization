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
            bool[] check = new bool[20000000];
            List<RGBPixel> Distinctcolors = new List<RGBPixel>();
            for (int i = 0; i < Dcolors_height; i++)
            {
                for (int j = 0; j < Dcolors_width; j++)
                {
                    int res = ImageMatrix[i, j].red;
                    res = (res << 8) + ImageMatrix[i, j].green;
                    res = (res << 8) + ImageMatrix[i, j].blue;
                    if (check[res]) continue;
                    check[res] = true;
                    Distinctcolors.Add(ImageMatrix[i, j]);
                }
            }

            return Distinctcolors;
        }


        /*public void generatePaths(List<RGBPixel> Colors, ref double[,] Matrix)
        {
            for (int i = 0; i < Colors.Count; i++)
            {
                for (int y = i; y < Colors.Count; y++)
                {
                    Matrix[i, y] = calculateDistance(Colors[i], Colors[y]);
                    Matrix[y, i] = calculateDistance(Colors[i], Colors[y]);
                }
            }
        }*/


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
