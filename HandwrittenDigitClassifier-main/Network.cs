using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace neuralnetworkstuff
{
    [Serializable()]

    class Network
    {
       public  string FileName = @" D:/programming/visStudio/neuralnetworkstuff/bin/Network.bin";

       public  Node[][] nodes;
    //  public  InputNode[] inputNodes;
      public  double[] output;
        int[] structure;
        int batchSize = 30;
        int batch = 0;
        public Network(int[] structure)
        {
            output = new double[structure[structure.Length - 1]];
            nodes = new Node[structure.Length][];
            this.structure = structure;
            Random r = new Random();
            for (int i = 0; i < structure.Length ; i++)
            {
                nodes[i] = new Node[structure[i]];
                for (int n = 0; n < structure[i]; n++)
                {
                    if (i == 0)
                    {
                        nodes[i][n] = new Node();
                    }
                    else
                    {
                        nodes[i][n] = new Node(ref nodes[i-1],0,1,n);

                    }

                }
            }

                    for (int i = 0; i < nodes.Length-1; i++)
            {
                for (int n = 0; n < nodes[i].Length; n++)
                {
                    nodes[i][n].setNextLayer ( ref nodes[i + 1]);
                }
            }
        }
        public void changeInputs(double[] newInput)
        {
            if (newInput.Length != nodes[0].Length)
            {
                throw new System.ArgumentException("Input size does not equal inputLayerSize ", "newInput");

            }
            else
            {
                for(int i = 0; i < newInput.Length; i++)
                {
                    nodes[0][i].activation=newInput[i];
                //    System.Diagnostics.Debug.WriteLine(inputNodes[i].Output);

                }
            }
        }
        public void calculateNetwork()
        {
            for(int i = 0; i < nodes.Length; i++)
            {
                for (int n = 0; n < nodes[i].Length; n++)
                {
                    nodes[i][n].calculateOutput();
          //       System.Diagnostics.Debug.WriteLine(nodes[i].Length);

                }
            }
           // System.Diagnostics.Debug.WriteLine(nodes.GetLength(0));
          //  System.Diagnostics.Debug.WriteLine(structure.l);

            Node[] finalNodes =new Node[nodes[nodes.Length-1].Length];
            finalNodes = nodes[nodes.Length - 1];
            for (int i= 0; i < finalNodes.Length; i++)
            {
                output[i] = nodes[nodes.Length - 1][i].Activation;
            }
        }
        public void learn(double[] desired)
        {
            computeErrors(desired);
            if (nodes[nodes.Length-1].Length != desired.Length)
            {
                throw new System.ArgumentException("Input size does not equal inputLayerSize ", "newInput");

            }
            batch++;

            for (int i = nodes.Length-1; i > 0; i--)
            {
                for (int n = 0; n < nodes[i].Length; n++)
                {

                    nodes[i][n].add();
                    if (batch == batchSize)
                    {
                        nodes[i][n].adjustWeights();
                       
                        nodes[i][n].reset();
                    }
                }
            }
            if (batch == batchSize)
            {
                batch = 0;
            }

        }
        public void computeErrors(double[] desired)
        {
            if (nodes[nodes.Length-1].Length != desired.Length)
            {
                throw new System.ArgumentException("Input size does not equal inputLayerSize ", "newInput");

            }
            for (int i = nodes.Length - 1; i > 0; i--)
            {
                for (int n = 0; n < nodes[i].Length; n++)
                {
                    if (i == nodes.Length-1)
                    {
                        nodes[i][n].error(desired[n]);
                    }
                    else
                    {
                        nodes[i][n].error();
                    }
                }
            }
         
            
            
        }

     
    }
}
