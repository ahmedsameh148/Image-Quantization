using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class Cluster
    {
        public void calculate_claster(ref List<KeyValuePair<KeyValuePair<int, int>, double>> res, int k, ref int[] arr_clus)
        {
            int curClus = 1;
            int remainClus = res.Count() + 1;
            res = res.OrderBy(x => x.Value).ToList();
            arr_clus = new int[res.Count + 1];
            for (int i = 0; i < res.Count() + 1; i++)
            {
                if (remainClus == k) return;
                if (arr_clus[res[i].Key.Key] == 0 && arr_clus[res[i].Key.Value] == 0)
                {
                    arr_clus[res[i].Key.Value] = curClus;
                    arr_clus[res[i].Key.Key] = curClus;
                    curClus++;
                    remainClus--;
                }

                else if (arr_clus[res[i].Key.Key] != 0 && arr_clus[res[i].Key.Value] == 0)
                {
                    arr_clus[res[i].Key.Value] = arr_clus[res[i].Key.Key];
                    remainClus--;
                }
                else if (arr_clus[res[i].Key.Key] == 0 && arr_clus[res[i].Key.Value] != 0)
                {
                    arr_clus[res[i].Key.Key] = arr_clus[res[i].Key.Value];
                    remainClus--;
                }
                else
                {
                    int x = arr_clus[res[i].Key.Value];
                    int y = arr_clus[res[i].Key.Key];
                    for (int j = 0; j < res.Count + 1; j++)
                    {
                        if (arr_clus[j] == y)
                            arr_clus[j] = x;

                    }
                    remainClus--;
                }
            }

        }
        public void result_color(ref int[] arr_clus, ref List<RGBPixel> Colors, int k, ref RGBPixel[] new_Colors)
        {

            byte defult_sum = 0;
            RGBPixel x = new RGBPixel();
            new_Colors = new RGBPixel[arr_clus.Count() + 1];
            for (int j = 0; j < arr_clus.Count(); j++)
            {
                if (arr_clus[j] == 0)
                    new_Colors[j] = Colors[j];
            }

            for (int i = 1; i < k; i++)
            {
                for (int j = 0; j < arr_clus.Count(); j++)
                {
                    if (arr_clus[j] == i)
                    {

                        x.blue += Colors[j].blue;
                        x.red += Colors[j].red;
                        x.green += Colors[j].green;
                        defult_sum++;
                    }

                }
                x.green = (byte)(x.green - defult_sum);
                x.red = (byte)(x.red - defult_sum);
                x.blue = (byte)(x.blue - defult_sum);
                new_Colors[i].Equals(x);
                defult_sum = 0;

            }

        }
    }

}
