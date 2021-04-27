using NeuralNetworks.NeuralNetworks.Connections;
using NeuralNetworks.NeuralNetworks.Data;
using NeuralNetworks.NeuralNetworks.Layers;
using NeuralNetworks.NeuralNetworks.Networks;
using NeuralNetworks.NeuralNetworks.Networks.Trainings;
using NeuralNetworks.NeuralNetworks.Networks.Trainings.Neuroevolution;
using NeuralNetworks.NeuralNetworks.Neurons.ActivationFunctions;
using NeuralNetworks.Problems.Approximation;
using NeuralNetworks.Problems.Classification;
using System;

namespace NeuralNetworks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("EBP");
            /*      TrainingData trainingData = new TrainingData();
                  trainingData.Patterns.Add(new Pattern() { Inputs = new double[] { 0, 0 }, Outputs = new double[] { 0 } });
                  trainingData.Patterns.Add(new Pattern() { Inputs = new double[] { 0, 1 }, Outputs = new double[] { 1 } });
                  trainingData.Patterns.Add(new Pattern() { Inputs = new double[] { 1, 0 }, Outputs = new double[] { 1 } });
                  trainingData.Patterns.Add(new Pattern() { Inputs = new double[] { 1, 1 }, Outputs = new double[] { 0 } });*/

            /*     Series trainingData = new Series();
                 trainingData.Patterns.Add(new Pattern() { Inputs = new double[] { -1, -1 }, Outputs = new double[] { 1 } });
                 trainingData.Patterns.Add(new Pattern() { Inputs = new double[] { -1, 1 }, Outputs = new double[] { -1 } });
                 trainingData.Patterns.Add(new Pattern() { Inputs = new double[] { 1, -1 }, Outputs = new double[] { -1 } });
                 trainingData.Patterns.Add(new Pattern() { Inputs = new double[] { 1, 1 }, Outputs = new double[] { 1 } });*/

            /*      TrainingData trainingData = new TrainingData();
                  trainingData.Patterns.Add(new Pattern() { Inputs = new double[] { 0, 0, 0 }, Outputs = new double[] { 0 } });
                  trainingData.Patterns.Add(new Pattern() { Inputs = new double[] { 0, 0, 1 }, Outputs = new double[] { 1 } });
                  trainingData.Patterns.Add(new Pattern() { Inputs = new double[] { 0, 1, 0 }, Outputs = new double[] { 1 } });
                  trainingData.Patterns.Add(new Pattern() { Inputs = new double[] { 0, 1, 1 }, Outputs = new double[] { 0 } });
                  trainingData.Patterns.Add(new Pattern() { Inputs = new double[] { 1, 0, 0 }, Outputs = new double[] { 1 } });
                  trainingData.Patterns.Add(new Pattern() { Inputs = new double[] { 1, 0, 1 }, Outputs = new double[] { 0 } });
                  trainingData.Patterns.Add(new Pattern() { Inputs = new double[] { 1, 1, 0 }, Outputs = new double[] { 0 } });
                  trainingData.Patterns.Add(new Pattern() { Inputs = new double[] { 1, 1, 1 }, Outputs = new double[] { 1 } });*/

            Sin problem = new Sin();
    
            NeuralNetwork neuralNetwork = new NeuralNetwork(1);
            neuralNetwork.NeuralLayers.Add(new NeuralLayer(3, new BipolarSigmoidActivationFunction { Coefficient = 0.25 }));
            neuralNetwork.NeuralLayers.Add(new NeuralLayer(1, new LinearActivationFunction { Coefficient = 0.1 }));
            neuralNetwork.Build();

            neuralNetwork.Training = new LevenbergMarquardtTraining();
            neuralNetwork.Train(problem.trainingData, problem.trainingData,  Math.Pow(10, -1));

            Console.WriteLine("TEST");
            foreach(Pattern pattern in problem.testData.Patterns)
            {
                neuralNetwork.SetInput(pattern.Inputs);
                neuralNetwork.Feedforward();
                Array.ForEach(neuralNetwork.GetOutput(), Console.WriteLine);
            }
            
        }
    }
}
