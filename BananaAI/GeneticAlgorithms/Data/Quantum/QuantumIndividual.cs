using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QIGA.DataStructures
{
    class QuantumIndividual
    {
        public List<Register> Chromosome { get; set; }

        public QuantumIndividual(int chromosomeLenght, int registerLenght)
        {
            this.Chromosome = new List<Register>();
            for (int i = 0; i < chromosomeLenght/registerLenght; i++) this.Chromosome.Add(new Register(registerLenght));
        }
  
    }
}
