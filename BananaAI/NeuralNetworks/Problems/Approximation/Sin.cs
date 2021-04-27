using NeuralNetworks.NeuralNetworks.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworks.Problems.Approximation
{
    class Sin
    {
        public Series trainingData;
        public Series testData;

        public Sin()
        {
            this.trainingData = new Series();
            this.trainingData.AddPattern(new double[] {0}, new double[] {Math.Sin(0)});
            this.trainingData.AddPattern(new double[] {Math.PI/6}, new double[] {Math.Sin(Math.PI / 6)});
            this.trainingData.AddPattern(new double[] { Math.PI / 3 }, new double[] { Math.Sin(Math.PI / 3) });
            this.trainingData.AddPattern(new double[] { Math.PI / 4 }, new double[] { Math.Sin(Math.PI / 4) });
            this.trainingData.AddPattern(new double[] { Math.PI }, new double[] { Math.Sin(Math.PI) });
            this.trainingData.AddPattern(new double[] { 2 * Math.PI}, new double[] { Math.Sin(2*Math.PI) });
            this.trainingData.AddPattern(new double[] { 7 * Math.PI / 6 }, new double[] { Math.Sin(7 * Math.PI / 6) });
            this.trainingData.AddPattern(new double[] { 4 * Math.PI / 3 }, new double[] { Math.Sin(4 * Math.PI / 3) });
            this.trainingData.AddPattern(new double[] { 5 * Math.PI / 4 }, new double[] { Math.Sin(5 * Math.PI / 4) });
            this.trainingData.AddPattern(new double[] { 5 * Math.PI / 6 }, new double[] { Math.Sin(5 * Math.PI / 6) });
            this.trainingData.AddPattern(new double[] { 2 * Math.PI / 3 }, new double[] { Math.Sin(2 * Math.PI / 3) });
            this.trainingData.AddPattern(new double[] { 3 * Math.PI / 4 }, new double[] { Math.Sin(3 * Math.PI / 4) });
            this.trainingData.AddPattern(new double[] { 5 * Math.PI / 3 }, new double[] { Math.Sin(5 * Math.PI / 3) });
            this.trainingData.AddPattern(new double[] { 11 * Math.PI / 6 }, new double[] { Math.Sin(11 * Math.PI / 6) });
            this.trainingData.AddPattern(new double[] { 7 * Math.PI / 4 }, new double[] { Math.Sin(7 * Math.PI / 4) });

            this.testData = new Series();
            for(double x = 0; x<=Math.PI*2; x+=0.1)
            {
                this.testData.AddPattern(new double[] { x }, new double[] { Math.Sin(x)});
            }
        }
    }
}
