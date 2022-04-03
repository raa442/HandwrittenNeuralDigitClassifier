using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace neuralnetworkstuff
{
    class DataImage
    {
       public double[] image=new double[784];
       public int value;
     public   DataImage(double[] im,int val)
        {
            image = im;
            value = val;
        }
    }
}
