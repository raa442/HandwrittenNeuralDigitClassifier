using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuralnetworkstuff
{
    [Serializable()]

    class Node
    {
     double   rate=90;


        public double delta = 0;
        public double deltaSum = 0;
        public double biasSum = 0;

        private double[] weights;
      private  double bias;
        private int type;//0-->pereptron,  1-->sigmoid
        public double activation;
        public double z;

        public Node[] input;
       public Node[] nextLayer;
        private int location;
        private double[] derivativesRweights;
        bool hasInput;
        
      public  double Activation
        {
            get { return activation; }
        }

         public   Node(ref Node[] inputs,double[] weights, double bias, int type, int location)
        {
            if (inputs.Length != weights.Length)
            {
                throw new System.ArgumentException("Input size does not equal weight size", "input");

            }
            input = inputs;
            this.weights = new double[weights.Length];
            for (int i = 0; i < weights.Length; i++)
            {
                this.weights[i] = weights[i];
            }
            this.bias = bias;
            this.type = type;
            this.location = location;
            derivativesRweights = new double[input.Length];
            hasInput = true;
        }
        public Node(ref Node[] inputs, double bias, int type, int location)
        {
          //  if (inputs.Length != input.Length)
           // {
            //    throw new System.ArgumentException("Input size does not equal weight size", "input");

//            }
            input = inputs;
            Random r = new Random();

            this.weights = new double[inputs.Length];
            for (int i = 0; i < input.Length; i++)
            {
                this.weights[i] = ((double)r.NextDouble() - 0.5)*2.0 ;
            }
            this.bias = ((double) r.NextDouble()-0.5)*2.0 ;
            this.type = type;
            this.location = location;
            derivativesRweights = new double[input.Length];
            hasInput = true;
        }
        public Node()
        {
            hasInput = false;
        }
         
        public void setNextLayer(ref Node[] nl)
        {
            this.nextLayer = nl;
        }
        public void calculateOutput()
        {

            if (hasInput)
            {
                if (input.Length != weights.Length)
                {
                    throw new System.ArgumentException("Input size does not equal weight size", "input");

                }
                double sum = 0;
                for (int i = 0; i < weights.Length; i++)
                {
                    sum += weights[i] * input[i].Activation;
                }
              sum += bias;
                //  switch (type)
                // {
                //    case 0:
                //       z = sum;
                //      activation = sum <= 0 ? 1 : 0;
                //     break;
                // case 1:
                z = sum;
                activation =  Math.Tanh(sum);
                //      break;
                //}

            }
           


        }
        public void error()
        {
            double errorSum = 0;
            for(int i = 0; i < nextLayer.Length; i++)
            {
                errorSum += nextLayer[i].delta * nextLayer[i].weights[this.location];
            }
            delta =(double) Math.Pow(1.0/Math.Cosh(z),2) * errorSum;
        }
        public void error(double desired)
        {
            delta =-1.0f* (desired - activation) * Math.Pow(1.0 / Math.Cosh(z), 2);
        }
        public void adjustWeights()
        {
            for(int i = 0; i < derivativesRweights.Length; i++)
            {
              //  derivativesRweights[i] = delta * input[i].Activation;
                weights[i] -= rate * derivativesRweights[i]/30.0f;
              //  System.Diagnostics.Debug.WriteLine(weights[i]);

            }
            bias-=biasSum/30.0f ;


        }
        public void add()
        {
            for (int i = 0; i < derivativesRweights.Length; i++)
            {
                 derivativesRweights[i] += delta * input[i].Activation;
            //    weights[i] -= rate * derivativesRweights[i];
                //  System.Diagnostics.Debug.WriteLine(weights[i]);

            }
            biasSum += rate * delta;


        }
        public void reset()
        {
            for (int i = 0; i < derivativesRweights.Length; i++)
            {
                derivativesRweights[i] = 0;
              //  weights[i] -= rate * derivativesRweights[i];
                //  System.Diagnostics.Debug.WriteLine(weights[i]);

            }
            biasSum = 0;
            //bias -= rate * delta;


        }

    }
}
