using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NeuralNetworks.NeuralNetworks.Data;
using QIGA.Algorithm;

namespace NeuralNetworks.NeuralNetworks.Networks.Trainings.Neuroevolution
{
    class QIGA2Training : ITraining
    {
        public void Train(NeuralNetwork neuralNetwork, Series trainingData, Series testData, double precision)
        {
            NeuralNetworkProblem problem = new NeuralNetworkProblem(neuralNetwork, trainingData, -34, 34);
            QIGA2 algorithm = new QIGA2(problem, 100, 180);
            algorithm.Solve(0);
            double SSE = 0;
            int count = 0;
            do
            {
                count++;
                algorithm.Generation();
                problem.Evaluate(algorithm.bestIndividual.Chromosome);
                SSE = this.CalculateTotalError(neuralNetwork, testData);

                Console.WriteLine(count + "; " + SSE + "; " + problem.GetSynapses(neuralNetwork).Count(x => x.Weight != 0));
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Szymon\Desktop\inzynierka\iteracja_2\pomiary\XOR\QIGA2\weights\w6.txt", true))
                {
                    file.WriteLine(count + "; " + SSE + "; " + problem.GetSynapses(neuralNetwork).Count(x => x.Weight!=0));
                }

            } while (SSE > precision );
            Console.WriteLine(SSE);
            
            problem.Evaluate(algorithm.bestIndividual.Chromosome);
        }

        private double CalculateTotalError(NeuralNetwork neuralNetwork, Series testData)
        {
            double SSE = 0;
            foreach (Pattern pattern in testData.Patterns)
            {
                neuralNetwork.SetInput(pattern.Inputs);
                neuralNetwork.Feedforward();
                double[] errors = neuralNetwork.GetErrors(pattern.Outputs);
                SSE += this.GetSSE(errors);
            }
            return (float)SSE;
        }

        private double GetSSE(double[] errors) => errors.Sum(error => Math.Pow(error, 2)) * 0.5;
    }
}
