using NeuralNetworks.NeuralNetworks.Connections;
using NeuralNetworks.NeuralNetworks.Neurons.ActivationFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuralNetworks.NeuralNetworks.Neurons
{
    class Neuron
    {
        private readonly IActivationFunction _activationFunction;

        public List<Neurite> DendriteTerminals { get; set; }
        public List<Neurite> AxonTerminals { get; set; }
        public Signal Axon { get; set; }
        public Signal Dendrite { get; set; }

        public Neuron(IActivationFunction activationFunction)
        {
            this._activationFunction = activationFunction;
            this.DendriteTerminals = new List<Neurite> {new Neurite()};
            this.AxonTerminals = new List<Neurite>();
            this.Axon = new Signal();
            this.Dendrite = new Signal();
        }

        private double FeedforwardSum => this.DendriteTerminals.Sum(terminal => terminal.OutputSignal);

        private double BackpropagateSum => this.AxonTerminals.Sum(terminal => terminal.OutputSignal);

        private double Activation => _activationFunction.Activation(this.FeedforwardSum);

        private double Slope => _activationFunction.Slope(this.FeedforwardSum);

        public void Feedforward() => this.Axon.Data = this.Activation;

        public void Backpropagate() => this.Dendrite.Data = this.Slope * this.BackpropagateSum;
    }
}
