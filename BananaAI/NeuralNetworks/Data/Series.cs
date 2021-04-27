using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworks.NeuralNetworks.Data
{
    class Series
    {
        
        public List<Pattern> Patterns { get; set; }

        public Series()
        {
            this.Patterns = new List<Pattern>();
        }

        public void AddPattern(double[] inputs, double[] outputs) => this.Patterns.Add(new Pattern() { Inputs = inputs, Outputs = outputs});
    }
}
