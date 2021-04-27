using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NeuralNetworks.NeuralNetworks.Connections;
using NeuralNetworks.NeuralNetworks.Data;
using NeuralNetworks.NeuralNetworks.Layers;
using NeuralNetworks.NeuralNetworks.Neurons;

namespace NeuralNetworks.NeuralNetworks.Networks.Trainings
{
    class ErrorBackPropagationTraining : ITraining
    {
        public void Train(NeuralNetwork neuralNetwork, Series trainingData, Series testData, double precision)
        {
            double SSE;
            double learnCoefficient= 0.5;
            double count = 0;
            do
            {
                count++;
                foreach (Pattern pattern in trainingData.Patterns)
                {
                    neuralNetwork.SetInput(pattern.Inputs);
                    neuralNetwork.Feedforward();
                    double[] errors = neuralNetwork.GetErrors(pattern.Outputs);
                    for (int i=0; i<errors.Length; i++)
                    {
                        neuralNetwork.FeedbackLayer.Signals[i].Data = errors[i];
                    }
                    neuralNetwork.Backpropagate();

                    //update weights
                    foreach(NeuralLayer layer in neuralNetwork.NeuralLayers)
                    {
                        foreach(Neuron neuron in layer.Neurons)
                        {
                            foreach(Neurite dendriteTerminal in neuron.DendriteTerminals)
                            {
                                dendriteTerminal.Synapse.Weight += 2 * learnCoefficient * neuron.Dendrite.Data * dendriteTerminal.InputSignal.Data;
                            }
                        }
                    }  
                }

                SSE = 0;
                foreach (Pattern pattern in testData.Patterns)
                {
                    //forward
                    neuralNetwork.SetInput(pattern.Inputs);
                    neuralNetwork.Feedforward();
                    double[] errors = neuralNetwork.GetErrors(pattern.Outputs);
                    SSE += this.GetSSE(errors);
                }
                Console.WriteLine(count+"; "+ SSE);
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Szymon\Desktop\inzynierka\iteracja_2\pomiary\XOR\EBP22.txt", true))
                {
                    file.WriteLine(count + "; " + SSE);
                }
            } while (SSE > precision);
            
        }

        private double GetSSE(double[] errors) => errors.Sum(error => Math.Pow(error, 2)) * 0.5;
    }
}
