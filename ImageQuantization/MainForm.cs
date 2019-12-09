using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ImageQuantization
{
    public partial class MainForm : Form
    {
        string path;
        public MainForm()
        {
            InitializeComponent();
        }

        RGBPixel[,] ImageMatrix;

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
        
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
            txtWidth.Text = ImageOperations.GetWidth(ImageMatrix).ToString();
            txtHeight.Text = ImageOperations.GetHeight(ImageMatrix).ToString();
            List<RGBPixel> l = distinct_Colors();
            txtWidth.Text = l.Count.ToString();
        }

        private void btnGaussSmooth_Click(object sender, EventArgs e)
        {
            double sigma = double.Parse(txtGaussSigma.Text);
            int maskSize = (int)nudMaskSize.Value ;
            ImageMatrix = ImageOperations.GaussianFilter1D(ImageMatrix, maskSize, sigma);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }

        private void txtWidth_TextChanged(object sender, EventArgs e)
        {

        }
    }
}