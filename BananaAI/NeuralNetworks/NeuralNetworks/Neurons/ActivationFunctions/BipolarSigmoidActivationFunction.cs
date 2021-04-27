using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworks.NeuralNetworks.Neurons.ActivationFunctions
{
    class BipolarSigmoidActivationFunction : IActivationFunction
    {
        public double Coefficient { get; set; }
        public double Activation(double x) => (1 - Math.Exp(-this.Coefficient * x)) / (1 + Math.Exp(-this.Coefficient * x));
        public double Slope(double x) => this.Coefficient * (1 - Math.Pow(this.Activation(x),2));
    }
}

