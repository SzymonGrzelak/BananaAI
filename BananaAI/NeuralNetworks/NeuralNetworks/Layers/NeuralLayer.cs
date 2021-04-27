using NeuralNetworks.NeuralNetworks.Neurons;
using NeuralNetworks.NeuralNetworks.Neurons.ActivationFunctions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworks.NeuralNetworks.Layers
{
    class NeuralLayer
    {
        public List<Neuron> Neurons { get; set; }

        public NeuralLayer(int size, IActivationFunction activationFunction)
        {
            this.Neurons = new List<Neuron>();
            for (int i = 0; i < size; i++)
            {
                Neurons.Add(new Neuron(activationFunction));
            }
        }

        public void Feedforward() => this.Neurons.ForEach(neuron => neuron.Feedforward());
        public void Backpropagate() => this.Neurons.ForEach(neuron => neuron.Backpropagate());

    }
}
