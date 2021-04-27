using NeuralNetworks.NeuralNetworks.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworks.Problems.Classification
{
    class XOR
    {
        public Series trainingData;
        public Series testData;
        
        public XOR()
        {
            this.trainingData = new Series();
            this.trainingData.AddPattern(new double[] { -1, -1 }, new double[] { 1 });
            this.trainingData.AddPattern(new double[] { -1, 1 }, new double[] { -1 });
            this.trainingData.AddPattern(new double[] { 1, -1 }, new double[] { -1 });
            this.trainingData.AddPattern(new double[] { 1, 1 }, new double[] { 1 });

            this.testData = new Series();
            this.testData.AddPattern(new double[] { -1, -1 }, new double[] { 1 });
            this.testData.AddPattern(new double[] { -1, 1 }, new double[] { -1 });
            this.testData.AddPattern(new double[] { 1, -1 }, new double[] { -1 });
            this.testData.AddPattern(new double[] { 1, 1 }, new double[] { 1 });
        }
    }
}
