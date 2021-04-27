using NeuralNetworks.NeuralNetworks.Connections;
using NeuralNetworks.NeuralNetworks.Data;
using NeuralNetworks.NeuralNetworks.Layers;
using NeuralNetworks.NeuralNetworks.Networks.Trainings;
using NeuralNetworks.NeuralNetworks.Neurons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuralNetworks.NeuralNetworks.Networks
{
    class NeuralNetwork
    {
        public ITraining Training { get; set; }
        public SignalLayer InputLayer { get; set; }
        public List<NeuralLayer> NeuralLayers { get; set; }
        public SignalLayer FeedbackLayer { get; set; }

        public NeuralNetwork(int inputSize)
        {
            this.InputLayer = new SignalLayer(inputSize);
            this.NeuralLayers = new List<NeuralLayer>();
        }

        public void SetInput(double[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                this.InputLayer.Signals[i].Data = data[i];
            }
        }

        public double[] GetOutput()
        {
            //this.Feedforward();
            double[] result = new double[NeuralLayers.Last().Neurons.Count];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = NeuralLayers.Last().Neurons[i].Axon.Data;
            }
            return result;
        }

        public double[] GetErrors(double [] expectedValues)
        {
            double[] results = GetOutput();
            double[] errors = new double[results.Length];
            for(int i=0; i<results.Length; i++)
            {
                errors[i] = expectedValues[i] - results[i];
            }
            return errors;
        }

        public void Feedforward() => this.NeuralLayers.ForEach(layer => layer.Feedforward());

        public void Backpropagate() => Enumerable.Reverse(this.NeuralLayers).ToList().ForEach(layer => layer.Backpropagate());

        public void Build()
        {
          
            //connect input layer
            foreach (Neuron neuron in NeuralLayers.First().Neurons)
            { 
                foreach (Signal signal in InputLayer.Signals)
                {
                    neuron.DendriteTerminals.Add(new Neurite(signal, new Synapse() { Weight = this.GetRandomWeight()}));
                }
            }

            //connect feed forward
            for(int i = 1; i<NeuralLayers.Count; i++)
            {
                FullyConnect(NeuralLayers[i - 1], NeuralLayers[i]);
            }

            //create feedback layer
            this.FeedbackLayer = new SignalLayer(NeuralLayers.Last().Neurons.Count);

            //connect feedback layer
            for(int i=0; i< NeuralLayers.Last().Neurons.Count; i++)
            {
                NeuralLayers.Last().Neurons[i].AxonTerminals.Add(new Neurite(this.FeedbackLayer.Signals[i]));
            }
        }

        private void FullyConnect(NeuralLayer connectingFrom, NeuralLayer connectingTo)
        {
            foreach (var to in connectingTo.Neurons)
            {
                foreach (var from in connectingFrom.Neurons)
                {
                    Synapse synapse = new Synapse() { Weight = this.GetRandomWeight()};
                    to.DendriteTerminals.Add(new Neurite(from.Axon, synapse));
                    from.AxonTerminals.Add(new Neurite(to.Dendrite, synapse));
                }
            }
        }


      public void Train(Series trainingData, Series testData, double precision) => this.Training.Train(this, trainingData, testData, precision);
      private double GetRandomWeight()
        {
            Random random = new Random();
            double minimum = 0;
            double maximum = 1;
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
  
}
