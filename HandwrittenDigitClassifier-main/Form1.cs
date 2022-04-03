using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace neuralnetworkstuff
{
    public partial class Form1 : Form
    {
        private NN net;
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image= new Bitmap(28, 28, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        }
        public void setNet(NN n)
        {
            net = n;
        }
        public void setPic(Image image)
        {
            pictureBox1.Image = image;
        }
        public Image getPic()
        {
            return pictureBox1.Image;
        }

        private void brushDown(object sender, MouseEventArgs e)
        {
            Bitmap before = new Bitmap( pictureBox1.Image);
            int X =(int)( (((float)e.X) / 300.0f) *27f);
            int Y = (int)((((float)e.Y) / 300f) * 27f);

            before.SetPixel(Clamp(X,0,27), Clamp(Y, 0, 27), Color.White);
            if(before.GetPixel(Clamp(X, 0, 27), Clamp(Y + 1, 0, 27)) .Equals(Color.Black)) before.SetPixel(Clamp(X , 0, 27), Clamp(Y + 1, 0, 27), Color.Gray);
            if (before.GetPixel(Clamp(X+1, 0, 27), Clamp(Y, 0, 27)).Equals( Color.Black)) before.SetPixel(Clamp(X + 1, 0, 27), Clamp(Y, 0, 27), Color.Gray);
            if (before.GetPixel(Clamp(X -1, 0, 27), Clamp(Y, 0, 27)).Equals( Color.Black)) before.SetPixel(Clamp(X - 1, 0, 27), Clamp(Y , 0, 27), Color.Gray);
            if (before.GetPixel(Clamp(X, 0, 27), Clamp(Y - 1, 0, 27)).Equals(Color.Black)) before.SetPixel(Clamp(X , 0, 27), Clamp(Y - 1, 0, 27), Color.Gray);

            pictureBox1.Image = before;

            Program.runTest(net, this);
        }
        private static int Clamp(int value, int min, int max)
        {
            return (value < min) ? min : (value > max) ? max : value;
        }
        private void brushOn(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                brushDown(sender,e);

            
        }
        public Boolean isTesting()
        {
            return checkBox1.Checked;
        }

        public void setPrediction(String pred)
        {
            textBox1.Text = pred;
        }

        private void wipe(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(28, 28, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.train(net);
        }
    }
}
