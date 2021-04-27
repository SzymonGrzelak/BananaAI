using NeuralNetworks.NeuralNetworks.Connections;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworks.NeuralNetworks.Layers
{
    class SignalLayer
    {
        public List<Signal> Signals { get; set; }

        public SignalLayer(int size)
        {
            this.Signals = new List<Signal>();
            for (int i = 0; i < size; i++)
            {
                Signals.Add(new Signal());
            }
        }
    }
}
