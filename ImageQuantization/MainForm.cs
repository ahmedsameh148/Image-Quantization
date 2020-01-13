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
        Distictit_Colors getDistincitColors;
        List<RGBPixel> colors;
        List<KeyValuePair<KeyValuePair<int, int>, double>> edges;
        public void start()
        {
            getDistincitColors = new Distictit_Colors(ImageMatrix);
            //Get The Distincit Colors
            colors = getDistincitColors.getDistincitColors();
            //An Object To Calculate The Minimum Spanning Tree
            Prim prim = new Prim();
            //List To Save The Edges The Resulting Minimum Spanning Tree
            edges = new List<KeyValuePair<KeyValuePair<int, int>, double>>();
            //The Total Sum Of The MST
            double sum = prim.getMst(ref edges, colors.Count, colors);
            MessageBox.Show(edges.Count.ToString());
            //Display The # Distinct Colors In The Width's Text Box
            txtWidth.Text = (colors.Count - 1).ToString();
            //Display The Sum Of The MST In The Height's Text Box
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
            int K = int.Parse(ClusterNumber.Text);
            Cluster c = new Cluster(colors,ImageMatrix);
            c.getClusters(edges, colors.Count, K);
            ImageMatrix = c.replace();
            double sigma = double.Parse(txtGaussSigma.Text);
            int maskSize = (int)nudMaskSize.Value;
            ImageMatrix = ImageOperations.GaussianFilter1D(ImageMatrix, maskSize, sigma);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }

        private void txtWidth_TextChanged(object sender, EventArgs e)
        {

        }
    }
}