using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworks.NeuralNetworks.Neurons.ActivationFunctions
{
    class UnipolarSigmoidActivationFunction : IActivationFunction
    {
        public double Coefficient { get; set; }

        public double Activation(double x) => 1 / (1 + Math.Exp(-this.Coefficient * x));
        public double Slope(double x) => this.Coefficient * this.Activation(x) * (1 - this.Activation(x));
    }
}
