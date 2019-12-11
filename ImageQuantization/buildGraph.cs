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

        /// <summary>
        /// calculate power number
        /// </summary>
        /// <param name="Base">number</param>
        /// <param name="power">times multiply the number by itself </param>
        /// <returns> long number</returns>
        /// take theta (n/2)
        public long Power(long Base, long power)
        {
            if (power == 1) return Base;//theta(1) 
            if (power == 0) return 1;// theta(1)
            long res = Power(Base, power / 2); //theta(n/2)
            if (power % 2 == 1) return Base * res * res;// theta(1)
            return res * res;
        }


        /// <summary>
        ///  calculate distance between 2 color
        /// </summary>
        /// <param name="color1">color have 3 byte values: red, green and blue</param>
        /// <param name="color2">color have 3 byte values: red, green and blue</param>
        /// 
        public double calculateDistance(RGBPixel color1, RGBPixel color2) {
            return Square(Power(Math.Abs(color1.red - color2.red), 2) + Power(Math.Abs(color1.green - color2.green), 2) + Power(color1.blue - color2.blue, 2));
        }



        /// <summary>
        /// creat matrix graph and calculate distance
        /// </summary>
        /// <param name="colors">list distinct_Colors</param>
        /// <param name="ImagePath">Image file path</param>
        
        public void generatePaths(List<RGBPixel> Colors, ref double[,] Matrix)
        {
            // take O(D^2)
            for (int i = 0; i < Colors.Count; i++)
            {
                for (int y = i; y < Colors.Count; y++)
                {
                    // O(1)
                    Matrix[i, y] = calculateDistance(Colors[i], Colors[y]);
                    Matrix[y, i] = calculateDistance(Colors[i], Colors[y]);
                }
            }
        }


        

    }
}
