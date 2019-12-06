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

        public List<RGBPixel> getColors() {
            List<RGBPixel> l = new List<RGBPixel>();
            List<RGBPixel> tmp = new List<RGBPixel>();
            int width = ImageOperations.GetWidth(ImageMatrix);
            int height = ImageOperations.GetHeight(ImageMatrix);
            for (int i = 0; i < height; i++)
            {
                for (int y = 0; y < width; y++)
                {
                    bool ok = true;
                    for (int j=0; j<tmp.Count; j++) {
                        if (tmp[j].blue == ImageMatrix[i, y].blue && tmp[j].red == ImageMatrix[i, y].red && tmp[j].green == ImageMatrix[i, y].green) {
                            ok = false; break;
                        }
                    }
                    if (ok) l.Add(ImageMatrix[i, y]);
                    tmp.Add(ImageMatrix[i, y]);
                }
            }
            return l;
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
            List<RGBPixel> l = getColors();
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