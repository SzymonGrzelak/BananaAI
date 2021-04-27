using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QIGA.DataStructures.Classic
{
    class ClassicPopulation 
    {
        public List<ClassicIndividual> Individuals { get; set; }

        public ClassicPopulation(int size, int chromosomeLenght)
        {
            this.Individuals = new List<ClassicIndividual>();
            for (int i = 0; i < size; i++) this.Individuals.Add(new ClassicIndividual(chromosomeLenght));
        }

    }
}
