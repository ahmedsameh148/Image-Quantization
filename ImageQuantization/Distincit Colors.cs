using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class Distictit_Colors
    {
        private RGBPixel[,] ImageMatrix;
        public Distictit_Colors(RGBPixel[,] Image)
        {
            ImageMatrix = Image;
        }
        //Function To Get The Distincit Color From 2d Matrix
        public List<RGBPixel> getDistincitColors()
        {
            //Get The Width Of The Image
            int Dcolors_width = ImageOperations.GetWidth(ImageMatrix);
            //Get The Height Of The Image
            int Dcolors_height = ImageOperations.GetHeight(ImageMatrix);
            //Array To Check The Repeatition Of Colors
            int[] check = new int[20000000];
            //List To Save The Distincit Colors
            List<RGBPixel> Distinctcolors = new List<RGBPixel>();
            Distinctcolors.Add(new RGBPixel());
            //Looping Over All Pixels
            for (int i = 0; i < Dcolors_height; i++)
            {
                for (int j = 0; j < Dcolors_width; j++)
                {
                    //Merge The 3 Bytes Of The Red Green and Blue
                    int res = ImageMatrix[i, j].red;
                    res = (res << 8) + ImageMatrix[i, j].green;
                    res = (res << 8) + ImageMatrix[i, j].blue;
                    //Check If The Pixel Taken Befor
                    if (check[res] != 0) continue;
                    //Mark The Pixel As Taken
                    check[res] = Distinctcolors.Count;
                    //Add The Pixel as A Distinct Color
                    Distinctcolors.Add(ImageMatrix[i, j]);
                }
            }
            //Return The Distinct Colors
            return Distinctcolors;
        }
    }
}
