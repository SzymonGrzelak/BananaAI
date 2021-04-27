using NeuralNetworks.NeuralNetworks.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworks.Problems.Classification
{
    class Parity3
    {
        public Series trainingData;
        public Series testData;

        public Parity3()
        {
            this.trainingData = new Series();
            this.trainingData.AddPattern(new double[] { -1, -1, -1 }, new double[] { 1 });
            this.trainingData.AddPattern(new double[] { -1, -1 , 1 }, new double[] { -1 });
            this.trainingData.AddPattern(new double[] { -1, 1, -1 }, new double[] { -1 });
            this.trainingData.AddPattern(new double[] { -1, 1, 1 }, new double[] { 1 });
            this.trainingData.AddPattern(new double[] { 1, -1, -1 }, new double[] { -1 });
            this.trainingData.AddPattern(new double[] { 1, -1, 1 }, new double[] { 1 });
            this.trainingData.AddPattern(new double[] { 1, 1, -1 }, new double[] { 1 });
            this.trainingData.AddPattern(new double[] { 1, 1, 1 }, new double[] { -1 });

            this.testData = new Series();
            this.trainingData.AddPattern(new double[] { -1, -1, -1 }, new double[] { 1 });
            this.trainingData.AddPattern(new double[] { -1, -1, 1 }, new double[] { -1 });
            this.trainingData.AddPattern(new double[] { -1, 1, -1 }, new double[] { -1 });
            this.trainingData.AddPattern(new double[] { -1, 1, 1 }, new double[] { 1 });
            this.trainingData.AddPattern(new double[] { 1, -1, -1 }, new double[] { -1 });
            this.trainingData.AddPattern(new double[] { 1, -1, 1 }, new double[] { 1 });
            this.trainingData.AddPattern(new double[] { 1, 1, -1 }, new double[] { 1 });
            this.trainingData.AddPattern(new double[] { 1, 1, 1 }, new double[] { -1 });
        }
    }
}
