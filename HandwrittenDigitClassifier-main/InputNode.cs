using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuralnetworkstuff
{
    [Serializable()]
class InputNode:Node
    {

        double activation;
        public double Activation
        {
            get { return activation; }
        }

        public InputNode( ){        
            
        }
        public void setVal(double input)
        {
            activation=input;
        }
        
    }
}
