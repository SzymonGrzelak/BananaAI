using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworks.NeuralNetworks.Data
{
    class Pattern
    {
        public double[] Inputs { get; set; }
        public double[] Outputs { get; set; }
        public Tuple<double[], double[]> Data => new Tuple<double[], double[]>(this.Inputs, this.Outputs);
    }
}
