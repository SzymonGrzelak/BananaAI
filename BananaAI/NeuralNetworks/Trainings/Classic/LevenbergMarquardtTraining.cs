using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using NeuralNetworks.NeuralNetworks.Connections;
using NeuralNetworks.NeuralNetworks.Data;
using NeuralNetworks.NeuralNetworks.Layers;
using NeuralNetworks.NeuralNetworks.Neurons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuralNetworks.NeuralNetworks.Networks.Trainings
{
    class LevenbergMarquardtTraining : ITraining
    {
      
        public void Train(NeuralNetwork neuralNetwork, Series trainingData, Series testData, double maxError)
        {
            List<Synapse> synapses = this.GetSynapses(neuralNetwork);
            double SSE=0;
            double coefficientMi = 1;
            double count = 0;
            
            do
            {
                count++;
                Matrix<double> quasiHessianMatrix = Matrix<double>.Build.Dense(synapses.Count, synapses.Count);
                Vector<double> gradientVector = Vector<double>.Build.Dense(synapses.Count);
                SSE = 0;
                int coefficientM = 1;

                foreach (Pattern pattern in trainingData.Patterns)
                {
                    //forward
                    neuralNetwork.SetInput(pattern.Inputs);
                    neuralNetwork.Feedforward();

                    //backward
                    double[] errors = neuralNetwork.GetErrors(pattern.Outputs);
                    SSE += this.GetSSE(errors);
                    for(int i=0; i<neuralNetwork.FeedbackLayer.Signals.Count; i++)
                    {
                        neuralNetwork.FeedbackLayer.Signals.ForEach(signal => signal.Data = 0);
                        neuralNetwork.FeedbackLayer.Signals[i].Data = -1;
                        neuralNetwork.Backpropagate();
                        Vector<double> jVector = this.GetJVector(neuralNetwork);
                        quasiHessianMatrix = quasiHessianMatrix.Add(this.GetQuasiHessianSubmatrix(jVector));
                        gradientVector = gradientVector.Add(this.GetGradientSubvector(jVector, errors[i]));
                    }

                }
                //compute delta weights
                
                Vector<double> deltaWeights = ((quasiHessianMatrix
                                                 .Add(Matrix<double>.Build.DenseIdentity(synapses.Count).Multiply(coefficientMi)))
                                                 .Inverse())
                                                 .Multiply(gradientVector);

                this.UpdateWeigths(neuralNetwork, deltaWeights.ToArray());

                double nextSSE = 0;
                foreach (Pattern pattern in trainingData.Patterns)
                {
                    //forward
                    neuralNetwork.SetInput(pattern.Inputs);
                    neuralNetwork.Feedforward();
                    double[] errors = neuralNetwork.GetErrors(pattern.Outputs);
                    nextSSE += this.GetSSE(errors);
                }

                while (coefficientM <= 5 && nextSSE >= SSE)
                {
                    this.RestoreWeigths(neuralNetwork, deltaWeights.ToArray());
                    coefficientMi *= 10;
                    coefficientM++;
                    deltaWeights = ((quasiHessianMatrix
                                                 .Add(Matrix<double>.Build.DenseIdentity(synapses.Count).Multiply(coefficientMi)))
                                                 .Inverse())
                                                 .Multiply(gradientVector);
                    this.UpdateWeigths(neuralNetwork, deltaWeights.ToArray());
                    nextSSE = 0;
                    foreach (Pattern pattern in trainingData.Patterns)
                    {
                        //forward
                        neuralNetwork.SetInput(pattern.Inputs);
                        neuralNetwork.Feedforward();
                        //backward
                        double[] errors = neuralNetwork.GetErrors(pattern.Outputs);
                        nextSSE += this.GetSSE(errors);
                    }
                }

                if (nextSSE < SSE)
                {
                        coefficientMi /= 10;
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

            } while (SSE > maxError);

            Console.WriteLine(count);

            Console.WriteLine("wagi:");
            foreach (Synapse s in synapses)
            {
                Console.WriteLine(s.Weight);
            }
        }

        private List<Synapse> GetSynapses(NeuralNetwork neuralNetwork)
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

        private Vector<double> GetGradientSubvector(Vector<double> jVector, double error) => jVector.Multiply(error);

        private Matrix<double> GetQuasiHessianSubmatrix(Vector<double> jVector) => Vector.OuterProduct(jVector, jVector);
        
        private Vector<double> GetJVector(NeuralNetwork neuralNetwork)
        {
            List<double> jVector = new List<double>();
            foreach(NeuralLayer layer in neuralNetwork.NeuralLayers)
            {
                foreach(Neuron neuron in layer.Neurons)
                {
                    foreach(Neurite dendriteTerminal in neuron.DendriteTerminals)
                    {
                        jVector.Add(dendriteTerminal.InputSignal.Data * neuron.Dendrite.Data);
                    }
                }
            }
            return Vector<double>.Build.DenseOfArray(jVector.ToArray());
        }


        private void UpdateWeigths(NeuralNetwork neuralNetwork, double[] deltaWeights)
        {
            int i = 0;
            foreach (Synapse synapse in this.GetSynapses(neuralNetwork))
            {
                
                synapse.Weight -= deltaWeights[i];
                i++;
            }
        }

        private void RestoreWeigths(NeuralNetwork neuralNetwork, double[] deltaWeights)
        {
            int i = 0;
            foreach (Synapse synapse in this.GetSynapses(neuralNetwork))
            {
                synapse.Weight += deltaWeights[i];
                i++;
            }
        }


        private double GetSSE(double[] errors) => errors.Sum(error => Math.Pow(error, 2)) * 0.5;
    }
}
