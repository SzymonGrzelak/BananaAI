using NeuralNetworks.NeuralNetworks.Connections;
using NeuralNetworks.NeuralNetworks.Data;
using NeuralNetworks.NeuralNetworks.Layers;
using NeuralNetworks.NeuralNetworks.Networks;
using NeuralNetworks.NeuralNetworks.Neurons;
using QIGA.Problems;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuralNetworks.NeuralNetworks.Networks.Trainings.Neuroevolution
{
    class NeuralNetworkProblem : IProblem<BitArray, float>
    {
        NeuralNetwork neuralNetwork;
        Series trainingData;
        List<Synapse> synapses;

        private float minWeight;
        private float maxWeight;

        public NeuralNetworkProblem(NeuralNetwork neuralNetwork, Series trainingData, float minWeight, float maxWeight)
        {
            this.neuralNetwork = neuralNetwork;
            this.trainingData = trainingData;
            this.synapses = this.GetSynapses(neuralNetwork);
            this.minWeight = minWeight;
            this.maxWeight = maxWeight;
        }

        public float Evaluate(BitArray individual)
        {
            //load individual into network
            int geneLength = individual.Length / synapses.Count;
            
            var singleWeightBitArrays = Enumerable
            .Range(0, synapses.Count)
            .Select(offset => CopySlice(individual, offset * geneLength, geneLength))
            .ToList();

            int i = 0;
            foreach(Synapse synapse in synapses)
            {
                synapse.Weight = DecodeWeight(singleWeightBitArrays[i]);
                i++;
            }
           
            return -CalculateTotalError();    
        }

        private float CalculateTotalError()
        {
            double SSE = 0;
            foreach (Pattern pattern in trainingData.Patterns)
            {
                //forward
                neuralNetwork.SetInput(pattern.Inputs);
                neuralNetwork.Feedforward();
                double[] errors = neuralNetwork.GetErrors(pattern.Outputs);
                SSE += this.GetSSE(errors);
            }
            return (float)SSE;
        }

        private double GetSSE(double[] errors) => errors.Sum(error => Math.Pow(error, 2)) * 0.5;

        public double DecodeWeight(BitArray codedWeight)
        {
            double result = 0;
            // result = Convert.ToInt32(codedWeight.Get(codedWeight.Count-1)) * Getx(codedWeight.LeftShift(2) ,this.minWeight,this.maxWeight);
           result = Getx(codedWeight, this.minWeight, this.maxWeight);
            return result;
        }

        public float Getx(BitArray individual, float min, float max)
        {
            float x = Bin2dec(individual);
            x = min + x * (max - min) / (MathF.Pow(2, individual.Count) - 1);
            return x;
        }

        private int Bin2dec(BitArray bitArray)
        {
            var result = new int[1];
            bitArray.CopyTo(result, 0);
            return result[0];
        }

        public static BitArray CopySlice(BitArray source, int offset, int length)
        {
            // Urgh: no CopyTo which only copies part of the BitArray
            BitArray ret = new BitArray(length);
            for (int i = 0; i < length; i++)
            {
                ret[i] = source[offset + i];
            }
            return ret;
        }

        public List<Synapse> GetSynapses(NeuralNetwork neuralNetwork)
        {
            List<Synapse> synapses = new List<Synapse>();
            foreach (NeuralLayer layer in neuralNetwork.NeuralLayers)
            {
                foreach (Neuron neuron in layer.Neurons)
                {
                    foreach (Neurite dendriteTerminal in neuron.DendriteTerminals)
                    {
                        synapses.Add(dendriteTerminal.Synapse);
                    }
                }
            }
            return synapses;
        }

    }
}
