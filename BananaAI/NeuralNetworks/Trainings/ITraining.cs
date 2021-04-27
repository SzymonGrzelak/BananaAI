using NeuralNetworks.NeuralNetworks.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworks.NeuralNetworks.Networks.Trainings
{
    interface ITraining
    {
        void Train(NeuralNetwork neuralNetwork, Series trainingData, Series testData, double precision);
    }
}
