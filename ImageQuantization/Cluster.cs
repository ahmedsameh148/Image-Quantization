using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class Cluster
    {
        List<RGBPixel> Clusters = new List<RGBPixel>();
        bool[] vis;
        List<int>[] adjList;
        int counter = 0;
        RGBPixel Pixel;
        List<RGBPixel> Colors;
        int[] guid;
        int curCluster;
        RGBPixel[,] ImageMatrix;
        int sum1, sum2, sum3;
        public Cluster(List<RGBPixel>l, RGBPixel[,]matrix) {
            Colors = l;
            ImageMatrix = matrix;
        }
        void Dfs(int node)
        {
            vis[node] = true;
            counter++;
            guid[node] = curCluster;
            sum1 += Colors[node].red;
            sum2 += Colors[node].green;
            sum3 += Colors[node].blue;

            for (int i = 0; i < adjList[node].Count; i++)
            {
                if (!vis[adjList[node][i]]) Dfs(adjList[node][i]);
            }
        }
        public void getClusters(List<KeyValuePair<KeyValuePair<int, int>, double>> edges, int D, int K)
        {
            MergeSort(edges, 0, edges.Count - 1);
            adjList = new List<int>[D + 1];
            vis = new bool[D + 1];
            guid = new int[D + 1];
            int currentClusters = D - 1, i = 0;
            for (int j = 0; j < D; j++) { adjList[j] = new List<int>(); }
            while (currentClusters > K)
            {
                KeyValuePair<int, int> Edge = new KeyValuePair<int, int>(edges[i].Key.Key, edges[i].Key.Value);
                adjList[Edge.Key].Add(Edge.Value);
                adjList[Edge.Value].Add(Edge.Key);
                i++; currentClusters--;
            }
            curCluster = 1;
            Clusters.Add(new RGBPixel());
            for (int j = 1; j < D; j++)
            {
                if (!vis[j]) {
                    counter = 0;
                    sum1 = 0; sum2 = 0; sum3 = 0;
                    Dfs(j);
                    sum1 /= counter;
                    sum2 /= counter;
                    sum3 /= counter;
                    RGBPixel tmp = new RGBPixel();
                    tmp.red = (byte)sum1;
                    tmp.green = (byte)sum2;
                    tmp.blue = (byte)sum3;
                    curCluster++;
                    Clusters.Add(tmp);
                }
            }
        }

        public RGBPixel[,] replace()
        {
            int Dcolors_width = ImageOperations.GetWidth(ImageMatrix);
            //Get The Height Of The Image
            int Dcolors_height = ImageOperations.GetHeight(ImageMatrix);
            for (int i = 0; i < Dcolors_height; i++)
            {
                for (int j = 0; j < Dcolors_width; j++)
                {
                    int res = ImageMatrix[i, j].red;
                    res = (res << 8) + ImageMatrix[i, j].green;
                    res = (res << 8) + ImageMatrix[i, j].blue;
                    int indexcheck = Distictit_Colors.check[res];
                    int indexCluster = guid[indexcheck];
                    ImageMatrix[i, j] = Clusters[indexCluster];
                }
            }
            return ImageMatrix;
        }

        private void Merge(List<KeyValuePair<KeyValuePair<int, int>, double>> input, int left, int middle, int right)
        {
            KeyValuePair<KeyValuePair<int, int>, double>[] leftArraytmp = new KeyValuePair<KeyValuePair<int, int>, double>[middle - left + 1];
            KeyValuePair<KeyValuePair<int, int>, double>[] rightArraytmp = new KeyValuePair<KeyValuePair<int, int>, double>[right - middle];

            input.CopyTo(left, leftArraytmp, 0, middle - left + 1);
            input.CopyTo(middle + 1, rightArraytmp, 0, right - middle);

            int i = 0;
            int j = 0;
            for (int k = left; k < right + 1; k++)
            {
                if (i == leftArraytmp.Length)
                {
                    input[k] = rightArraytmp[j];
                    j++;
                }
                else if (j == rightArraytmp.Length)
                {
                    input[k] = leftArraytmp[i];
                    i++;
                }
                else if (leftArraytmp[i].Value <= rightArraytmp[j].Value)
                {
                    input[k] = leftArraytmp[i];
                    i++;
                }
                else
                {
                    input[k] = rightArraytmp[j];
                    j++;
                }
            }
        }

        private void MergeSort(List<KeyValuePair<KeyValuePair<int, int>, double>> input, int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;

                MergeSort(input, left, middle);
                MergeSort(input, middle + 1, right);

                Merge(input, left, middle, right);
            }
        }
    }

}
