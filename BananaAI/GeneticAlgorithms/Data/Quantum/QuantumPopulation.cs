using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QIGA.DataStructures
{
    class QuantumPopulation
    {
        public List<QuantumIndividual> Individuals { get; set; }

        public QuantumPopulation(int size, int chromosomeLenght, int registerLenght) 
        {
            this.Individuals = new List<QuantumIndividual>();
            for (int i = 0; i < size; i++) this.Individuals.Add(new QuantumIndividual(chromosomeLenght,registerLenght));
        }

    }
}
