using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace neuralnetworkstuff
{

    static class Program
    {
      static  bool getSaved = true  ;
        static int dataLength = 10000;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DataImage[] data = new DataImage[dataLength];

             //  data = Helpers.getData("D:/programming/visStudio/neuralnetworkstuff/train",dataLength);
               data = Helpers.getData("D:/programming/visStudio/neuralnetworkstuff/t10k",dataLength);

            Console.WriteLine(5);
            int noCorrect=0;
            double[] x = new double[784];
            for(int k=0;k<x.Length;k++)
            {
                x[k] = -100f;
            }
        //    data[0].image = x;
    //       System.Diagnostics.Debug.WriteLine(Helpers.sigmoid(1.0f));

     //    System.Diagnostics.Debug.WriteLine("HIIIIIIIIIIIIIIIIIIIIIIIII");
            int[] str = { 784,64,64 ,10};

            //    Network n =new Network(str);
            NN net = new NN(str);
            if (getSaved == true)
            {
                net = Helpers.returnSavedNetwork(net.FileName);
            }
            Random r = new Random();
            for (int n = 0; n < 6; n++)
            {
                for (int i = 0; i < dataLength; i++)
                {
                    double[] d = { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
                  //  Helpers.saveNetwork(net, net.FileName);

                    //    Console.WriteLine(data[i].value);
                    //  double[]   b ={ (double)r.NextDouble(),(double)r.NextDouble()};
                    d[data[i].value] = 1f;
                    net.forward(data[i].image);
                 //   net.backProp(d);
                    //       n.changeInputs(data[i].image);

                    //     n.calculateNetwork();
                    //   n.learn(d);
                    if(data[i].value==net.activations[str.Length-1].ToList().IndexOf(net.activations[str.Length - 1].Max()))
                    {
                        noCorrect++;
                    }
                    ///System.Diagnostics.Debug.WriteLine(data[i].value);
                    // System.Diagnostics.Debug.WriteLine(i);

                    // foreach (double f in net.activations[str.Length - 1])
                    //   {
                    //   System.Diagnostics.Debug.WriteLine(f);
                    // }
                    //   System.Diagnostics.Debug.WriteLine("next");
                    if (i % (dataLength/10) == 0)
                    {
                        Console.WriteLine(" . ");

                    }
                }
                Console.WriteLine("EPOCH COMPLETE" + n.ToString());
                Console.WriteLine("accuracy: " + (100.0 * ((((double)noCorrect) /((double)dataLength)))).ToString());
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                noCorrect = 0;
                Helpers.saveNetwork(net, net.FileName);

            }

            //a.
            //    n.calculateOutput();
            System.Drawing.Bitmap bitm = new System.Drawing.Bitmap(28, 28, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                            for (int i = 0; i < 28; i++)
                           {
                              for (int j = 0; j < 28; j++)
                             {
                    bitm.SetPixel(i,j, System.Drawing.Color.FromArgb((int)(data[4].image[i + j * 28] * 255.0f), 255, 0) );
                           }

                       }
            System.Diagnostics.Debug.WriteLine(data[4].value);

            bitm.Save("D:/programming/visStudio/neuralnetworkstuff/t1.png");
       
         //   foreach (double f in n.output)
          //  {
           //     System.Diagnostics.Debug.WriteLine(f);
            //}
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }
    }
}
