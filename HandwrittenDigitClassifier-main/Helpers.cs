using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace neuralnetworkstuff
{
    static class Helpers
    {

        public static double sigmoid(double a)
        {
            return 1.0f / (1.0f + (double)Math.Exp(-4.0 * a));
        }
        public static double sigmoidDer(double a)
        {
            return 4.0 * Math.Exp(-a * 4.0) * Math.Pow((1.0 + Math.Exp(-4.0 * a)), -2);
        }
        public static void saveNetwork(Object network, string FileName)
        {
            Stream SaveFileStream = File.Create(FileName);
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(SaveFileStream, network);
            SaveFileStream.Close();
        }

        public static NN returnSavedNetwork(string FileName)
        {
            if (File.Exists(FileName))
            {
                Console.WriteLine("Reading saved file");
                Stream openFileStream = File.OpenRead(FileName);
                BinaryFormatter deserializer = new BinaryFormatter();
                NN net = (NN)deserializer.Deserialize(openFileStream);
                openFileStream.Close();
                return net;
            }
            else
            {
                throw new System.ArgumentException("no file found", "filename");

            }

        }
        public static DataImage[] getData(string filePath,int length)
        {
            String imgFileName = filePath + "-images.idx3-ubyte";
            String lblFileName = filePath + "-labels.idx1-ubyte";
            DataImage[] outp;

            using (FileStream fs = File.OpenRead(imgFileName))
            {
                using (FileStream lb = File.OpenRead(lblFileName))
                {
                    byte[] b = new byte[4];
                    fs.Read(b, 0, 4);
                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(b);
                    int mag = BitConverter.ToInt32(b, 0);



                    fs.Read(b, 0, 4);
                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(b);
                    int nImages = BitConverter.ToInt32(b, 0);



                    outp = new DataImage[nImages];


                    fs.Read(b, 0, 4);
                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(b);
                    int rows = BitConverter.ToInt32(b, 0);


                    fs.Read(b, 0, 4);
                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(b);
                    int cols = BitConverter.ToInt32(b, 0);


                    lb.Read(new byte[8], 0, 8);


                    byte[] c = new byte[1];
                    double[] dataSum = new double[rows * cols];
                    double[] dataMean = new double[rows * cols];
                    double[] datastd = new double[rows * cols];
                    double[][] dataVals = new double[length][];
                    double[][] dataValsNorm = new double[length][];

                    for (int i = 0; i < 784; i++)
                    {
                        dataSum[i] = 0;
                    }
                        for (int n = 0; n < length; n++)
                    {
                        dataVals[n] = new double[rows * cols];
                        dataValsNorm[n] = new double[rows * cols];
                        double[] data = new double[rows * cols];

                        for (int i = 0; i < 784; i++)
                        {

                            data[i] = ((double)((int)fs.ReadByte())) / 255.0-0.5;
                            dataVals[n][i] = data[i];


                        }
           

                    }

              
                    for (int n = 0; n < length; n++)
                    {
                        double[] data = new double[rows * cols];

                        for (int i = 0; i < 784; i++)
                        {
                            dataValsNorm[n][i] = (dataVals[n][i]);
                        }
                        outp[n] = new DataImage(dataValsNorm[n], lb.ReadByte());

                    }

                }
            
            }

            return outp;
        }
        public static double std(double mean, double[] data)
        {
            double sum = 0;

            for (int i = 0; i < data.Length; i++)
            {
                sum += Math.Pow(data[i] - mean, 2);
            }
            return Math.Sqrt(sum / data.Length);
        }

    }
}
