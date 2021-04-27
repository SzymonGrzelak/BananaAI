using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworks.NeuralNetworks.Neurons.ActivationFunctions
{
    class LinearActivationFunction : IActivationFunction
    {
        public double Coefficient { get; set; }
        public double Activation(double x) => this.Coefficient * x;
        public double Slope(double x) => this.Coefficient;
    }
}
