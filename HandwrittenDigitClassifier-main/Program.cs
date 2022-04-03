using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace neuralnetworkstuff
{

    static class Program
    {
        static bool getSaved = true;
        static int dataLength = 60000;
        static bool training = true;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            dataLength = training ? 10000 : 60000;

            DataImage[] data = new DataImage[dataLength];

            data = training ? Helpers.getData("./../../t10k", dataLength) : Helpers.getData("./../../train", dataLength);

            Console.WriteLine(5);
            int noCorrect = 0;
            double[] x = new double[784];
            for (int k = 0; k < x.Length; k++)
            {
                x[k] = -100f;
            }

            int[] str = { 784, 64, 64, 10 };

            NN net = new NN(str);
            if (getSaved == true)
            {
                net = Helpers.returnSavedNetwork(net.FileName);
            }
            Random r = new Random();
            for (int n = 0; n < (training ? 6 : 1); n++)
            {
                for (int i = 0; i < dataLength; i++)
                {
                    double[] d = { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };

                    d[data[i].value] = 1f;
                   
                    net.forward(data[i].image);
                   
                    if (training)
                        net.backProp(d);

                    if (data[i].value == net.activations[str.Length - 1].ToList().IndexOf(net.activations[str.Length - 1].Max()))
                    {
                        noCorrect++;
                    }

                    if (i % (dataLength / 10) == 0)
                    {
                        Console.WriteLine(" . ");

                    }
                }
                Console.WriteLine("EPOCH COMPLETE" + n.ToString());
                Console.WriteLine("accuracy: " + (100.0 * ((((double)noCorrect) / ((double)dataLength)))).ToString());
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                noCorrect = 0;
                Helpers.saveNetwork(net, net.FileName);

            }


            for (int k = 0; k < 784; k++)
            {
                Console.Write(data[4].image[k] >0 ? "$" : " ");
                if (k % 28 == 0)
                    Console.WriteLine("");
            }
            System.Drawing.Bitmap bitm = new System.Drawing.Bitmap(28, 28, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            for (int i = 0; i < 28; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    bitm.SetPixel(i, j, System.Drawing.Color.FromArgb((int)(127.0f + data[4].image[i + j * 28] * 255.0f), (int)(127.0f + data[4].image[i + j * 28] * 255.0f), (int)(127.0f + data[4].image[i + j * 28] * 255.0f)));
                }

            }
            bitm.Save("test.png", System.Drawing.Imaging.ImageFormat.Png);
            System.Diagnostics.Debug.WriteLine(data[4].value);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 f = new Form1();
            net.id = 1992;
            f.setNet(net);
           // f.setPic(bitm);

            Application.Run(f);
           
        }
        public static void train(NN net)
        {
            dataLength = 60000;

            DataImage[] data = new DataImage[dataLength];

            data = Helpers.getData("./../../train", dataLength);

            Console.WriteLine(5);
            int noCorrect = 0;
            double[] x = new double[784];
            for (int k = 0; k < x.Length; k++)
            {
                x[k] = -100f;
            }

            int[] str = { 784, 64, 64, 10 };

           
            Random r = new Random();
            for (int n = 0; n < (training ? 6 : 1); n++)
            {
                for (int i = 0; i < dataLength; i++)
                {
                    double[] d = { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };

                    d[data[i].value] = 1f;

                    net.forward(data[i].image);

                    if (training)
                        net.backProp(d);

                    if (data[i].value == net.activations[str.Length - 1].ToList().IndexOf(net.activations[str.Length - 1].Max()))
                    {
                        noCorrect++;
                    }

                    if (i % (dataLength / 10) == 0)
                    {
                        Console.WriteLine(" . ");

                    }
                }
                Console.WriteLine("EPOCH COMPLETE" + n.ToString());
                Console.WriteLine("accuracy: " + (100.0 * ((((double)noCorrect) / ((double)dataLength)))).ToString());
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                noCorrect = 0;
                Helpers.saveNetwork(net, net.FileName);

            }
        }
        public static void runTest(NN net, Form1 f)
        {
            if (f.isTesting())
            {//get image convert to -0.5 to 0.5. feed forward. take results
                Console.WriteLine(net.id);
                System.Drawing.Bitmap bp = new System.Drawing.Bitmap(f.getPic());
                double[] dataVals = new double[784];
                for (int i = 0; i < 784; i++)
                {
                    dataVals[i] = bp.GetPixel(i % 28, i / 28).R / 255.0 - 0.5;
                }
                for (int i = 0; i < 784; i++)
                {
                    Console.Write(dataVals[i]==0.5?"$":" ");
                    if (i % 28 == 0)
                        Console.WriteLine("");
                }
                net.forward(dataVals);
                string s = "";
                foreach(float fl in net.activations[3])
                {
                    s += Math.Round(fl,2).ToString()+" ";
                }
                f.setPrediction(s);
            }
        }
    }
}
