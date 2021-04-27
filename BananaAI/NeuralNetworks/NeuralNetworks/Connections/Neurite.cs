using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetworks.NeuralNetworks.Connections
{
    class Neurite
    {
        public Signal InputSignal { get; set; }
        public Synapse Synapse { get; set; }

        public double OutputSignal => this.InputSignal.Data * this.Synapse.Weight;

        public Neurite(Signal inputSignal = null, Synapse synapse = null)
        {
            this.InputSignal = inputSignal ?? new Signal {Data = 1};
            this.Synapse = synapse ?? new Synapse {Weight = 1};
        }

    }
}
