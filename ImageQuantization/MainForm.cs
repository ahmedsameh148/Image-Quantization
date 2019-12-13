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
        public MainForm()
        {
            InitializeComponent();
        }
        static RGBPixel[,] ImageMatrix;
        MST mst;
        buildGraph buildgraph;

        public void start()
        {
            mst = new MST(); 
            buildgraph = new buildGraph(ImageMatrix);
            List<RGBPixel> colors = buildgraph.distinct_Colors();
            /*double[,] Matrix = new double[colors.Count, colors.Count];
            buildgraph.generatePaths(colors, ref Matrix);
            double[,] resMatrix = new double[colors.Count, colors.Count];
            double sum = mst.ComputeMSTPath(Matrix, colors.Count, ref resMatrix);*/
            Prim prim = new Prim();
            List<KeyValuePair<KeyValuePair<int, int>, double>> edges = new List<KeyValuePair<KeyValuePair<int, int>, double>>();
            double sum = prim.getMst(0, ref edges, colors.Count, colors);
            txtWidth.Text = colors.Count.ToString();
            txtHeight.Text = sum.ToString();
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
            start();
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