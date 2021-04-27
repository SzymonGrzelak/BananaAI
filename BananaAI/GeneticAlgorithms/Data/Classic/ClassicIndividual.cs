using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QIGA.DataStructures.Classic
{
    public class ClassicIndividual
    {
        
        public BitArray Chromosome { get; set; }

        public ClassicIndividual(int chromosomeLenght)
        {
            this.Chromosome = new BitArray(chromosomeLenght);
        }
    }
}
