using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworks.NeuralNetworks.Neurons.ActivationFunctions
{
    interface IActivationFunction
    {
        double Activation(double x);
        double Slope(double x);
    }
}
