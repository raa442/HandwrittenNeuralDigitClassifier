using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuralnetworkstuff
{
    [Serializable()]

    class NN
    {
        public string FileName = @" D:/programming/visStudio/neuralnetworkstuff/bin/Networknnn.bin";

        private double[][][] weights;//layer,node to go to, node coming from
        public double[][] activations;// layer, node at
        private double[][] biasses;//layer, node at
        private double[][] deltas;//layer, node at 
        int[] layers;
        Random r;
        double rate = 0.05f;
        public NN(int[] struc)
        {
            this.layers = new int[struc.Length];
            for (int i = 0; i < struc.Length; i++)
            {
                this.layers[i] = struc[i];
            }
            r = new Random();
            InitActivations();
            InitDeltas();
            InitBiases();
            InitWeights();
        }

        public void forward(double[] input)
        {
            if (input.Length != layers[0])
                throw new System.ArgumentException("inputs are wrong length", "filename");

            for (int l = 0; l < layers.Length; l++)
            {
                for (int j = 0; j < layers[l]; j++)
                {
                    if (l == 0)
                    {
                        activations[0][j] = input[j];
                    }
                    else
                    {
                        activations[l][j] = (double)Helpers.sigmoid(dot(activations[l - 1], weights[l][j])+biasses[l][j]);

                    }
                }
            }
        }
        public void forward(int[] input)
        {
            if (input.Length != layers[0])
                throw new System.ArgumentException("inputs are wrong length", "filename");

            for (int l = 0; l < layers.Length; l++)
            {
                for (int j = 0; j < layers[l]; j++)
                {
                    if (l == 0)
                    {
                        activations[0][j] =(double) input[j];
                    }
                    else
                    {
                        activations[l][j] = (double)Helpers.sigmoid(dot(activations[l - 1], weights[l][j]) + biasses[l][j]);

                    }
                }
            }
        }

        public void backProp(double[] desired)
        {
            double erroradd = 0;
            
            for (int l = layers.Length - 1; l > 0; l--)
            {
                
                for (int j = 0; j < layers[l]; j++)
                {
                    if (l == layers.Length - 1)
                    {
                        deltas[l][j] = (activations[l][j] - desired[j]) * (1 - activations[l][j]) * (activations[l][j]);
                        erroradd += 0.5*Math.Pow(desired[j] - activations[l][j],2);
                    }
                    else
                    {
                        double[] temp=new double[activations[l+1].Length];
                        for (int k = 0; k < activations[l+1].Length; k++)
                        {
                            temp[k] = weights[l + 1][k][j];
                        }

                            deltas[l][j] = dot(deltas[l + 1], temp) * (1 - activations[l][j]) * (activations[l][j]);
                    }
                }
            }
         //   Console.Write("error: ");
           // Console.WriteLine(erroradd);

            for (int l = layers.Length - 1; l > 0; l--)
            {
                for (int j = 0; j <layers[l]; j++)
                {
                    biasses[l][j] -= rate * deltas[l][j];
                    for (int k = 0; k < layers[l-1]; k++)
                    {
                        weights[l][j][k] -= rate * activations[l - 1][k] * deltas[l][j];
                    }
                }
            }


                        }
        private void InitActivations()
        {
            List<double[]> activList = new List<double[]>();
            for (int i = 0; i < layers.Length; i++)
            {
                activList.Add(new double[layers[i]]);
            }
            activations = activList.ToArray();
        }
        private void InitDeltas()
        {
            List<double[]> activList = new List<double[]>();
            for (int i = 0; i < layers.Length; i++)
            {
                activList.Add(new double[layers[i]]);
            }
            deltas = activList.ToArray();
        }
        private void InitWeights()
        {
            List<double[][]> weightsList = new List<double[][]>();
            weightsList.Add((new List<double[]>()).ToArray());
            for (int l = 1; l < layers.Length; l++)
            {
                List<double[]> layerWeightsList = new List<double[]>();
                int neuronsInPreviousLayer = layers[l - 1];
                for (int j = 0; j < layers[l]; j++)
                {
                    double[] neuronWeights = new double[neuronsInPreviousLayer];
                    for (int k = 0; k < neuronsInPreviousLayer; k++)
                    {
                        neuronWeights[k] = r.NextDouble() - 0.5;
                    }
                    layerWeightsList.Add(neuronWeights);
                }
                weightsList.Add(layerWeightsList.ToArray());
            }
            weights = weightsList.ToArray();
        }
        private void InitBiases()
        {
            List<double[]> biasList = new List<double[]>();
            for (int i = 0; i < layers.Length; i++)
            {
                double[] bias = new double[layers[i]];
                for (int j = 0; j < layers[i]; j++)
                {
                    bias[j] = r.NextDouble() - 0.5;
                }
                biasList.Add(bias);
            }
            biasses = biasList.ToArray();
        }
        private double dot(double[] a, double[] b)
        {
            if (a.Length != b.Length)

                throw new System.ArgumentException("dotproductlengthsdontmatch", "filename");

            double sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                sum += a[i] * b[i];
            }
            return sum;
        }
    }
}
