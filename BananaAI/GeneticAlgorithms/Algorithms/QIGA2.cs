using QIGA.DataStructures;
using QIGA.DataStructures.Classic;
using QIGA.Problems;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace QIGA.Algorithm
{
    public class QIGA2
    {
        private QuantumPopulation quantumPopulation;
        private ClassicPopulation classicPopulation;
        private Random random = new Random();
        private IProblem<BitArray, float> problem;
        private float[] fval;
        public ClassicIndividual bestIndividual;
        private int populationSize;
        private int chromosomeLength;

        public QIGA2(IProblem<BitArray, float> problem, int populationSize, int chromosomeLength)
        {
            this.problem = problem;
            this.fval = new float[populationSize];
            this.populationSize = populationSize;
            this.chromosomeLength = chromosomeLength;
        }

        void Initialize(int populationSize, int chromosomeLength)
        {
            this.quantumPopulation = new QuantumPopulation(populationSize, chromosomeLength, 2);
            this.classicPopulation = new ClassicPopulation(populationSize, chromosomeLength);
        }

        void Observe()
        {
          
            for(int i=0; i<quantumPopulation.Individuals.Count; i++)
            {
                for (int j = 0; j < quantumPopulation.Individuals[i].Chromosome.Count; j++)
                {
                    double r = random.NextDouble();
                    if (r < Math.Pow(quantumPopulation.Individuals[i].Chromosome[j].Amplitudes[0], 2))
                    {
                        classicPopulation.Individuals[i].Chromosome[j*2] = false;
                        classicPopulation.Individuals[i].Chromosome[j*2+1] = false;          
                    }
                    else if(r< Math.Pow(quantumPopulation.Individuals[i].Chromosome[j].Amplitudes[0], 2)+ Math.Pow(quantumPopulation.Individuals[i].Chromosome[j].Amplitudes[1], 2))
                    {
                        classicPopulation.Individuals[i].Chromosome[j * 2] = true;
                        classicPopulation.Individuals[i].Chromosome[j * 2 + 1] = false;
                    }
                    else if(r< Math.Pow(quantumPopulation.Individuals[i].Chromosome[j].Amplitudes[0], 2) + Math.Pow(quantumPopulation.Individuals[i].Chromosome[j].Amplitudes[1], 2) + Math.Pow(quantumPopulation.Individuals[i].Chromosome[j].Amplitudes[2], 2))
                    {
                        classicPopulation.Individuals[i].Chromosome[j * 2] = false;
                        classicPopulation.Individuals[i].Chromosome[j * 2 + 1] = true;
                    }
                    else
                    {
                        classicPopulation.Individuals[i].Chromosome[j * 2] = true;
                        classicPopulation.Individuals[i].Chromosome[j * 2 + 1] = true;
                    }
                   
                }
            }


        }

        void Repair ()
        {
            for (int i = 0; i < classicPopulation.Individuals.Count; i++)
            {
                this.problem.Repair(classicPopulation.Individuals[i].Chromosome);
            }
        }

        void Evaluate()
        {
            for(int i=0; i<classicPopulation.Individuals.Count; i++)
            {   
                this.fval[i]=this.problem.Evaluate(classicPopulation.Individuals[i].Chromosome);
            }
        }

        void Update(ClassicIndividual bestIndividual)
        {
            for (int i = 0; i < quantumPopulation.Individuals.Count; i++)
            {
                for(int j=0; j < quantumPopulation.Individuals[i].Chromosome.Count; j++)
                {
                    double sum = 0;
                    int bestamp = Bin2dec(CopySlice(bestIndividual.Chromosome, 2*j, 2));
                    for (int amp = 0; amp < quantumPopulation.Individuals[i].Chromosome[j].Amplitudes.Count; amp++)
                    {
                        if(amp != bestamp)
                        {
                            quantumPopulation.Individuals[i].Chromosome[j].Amplitudes[amp] *= 0.95;
                            sum+=Math.Pow(quantumPopulation.Individuals[i].Chromosome[j].Amplitudes[amp], 2);
                        }
                    }
                    quantumPopulation.Individuals[i].Chromosome[j].Amplitudes[bestamp] = Math.Sqrt(1 - sum);
                }
            }
        }

        void Storebest()
        {
            int i;
            float val = this.fval[0];
            for (i = 0; i < classicPopulation.Individuals.Count; i++)
            {
                if (this.fval[i] >= val)
                {
                    val = this.fval[i];
                    this.bestIndividual = classicPopulation.Individuals[i];
                }
            }    
        }


        private BitArray CopySlice(BitArray source, int offset, int length)
        {
            BitArray ret = new BitArray(length);
            for (int i = 0; i < length; i++)
            {
                ret[i] = source[offset + i];
            }
            return ret;
        }
        private int Bin2dec(BitArray bitArray)
        {
            var result = new int[1];
            bitArray.CopyTo(result, 0);
            return result[0];
        }

        public float Getx(BitArray individual, float min, float max)
        {
            float x = Bin2dec(individual);
            x = min + x * (max - min) / (MathF.Pow(2, individual.Count) - 1);
            return x;
        }

        public void Solve(int tmax)
        {
            int t = 0;
            Initialize(this.populationSize, chromosomeLength);
            Observe();
            Repair();
            Evaluate();
            Storebest(); 
            while (t < tmax)
            {
                Generation();
                Console.WriteLine(this.problem.Evaluate(this.bestIndividual.Chromosome));
                t++;           
            };
            Console.WriteLine("best solution: ");
            Console.WriteLine("w przestrzeni problemu (oś x)");
            //Console.WriteLine(Getx(this.bestIndividual.Chromosome, -5, 5));
            Console.WriteLine("w przestrzeni problemu (funkcja przystosowania)");
            Console.WriteLine(this.problem.Evaluate(this.bestIndividual.Chromosome)); 
        }

        public void Generation()
        {
            Observe();
            Repair();
            Evaluate();
            Storebest(); 
            Update(this.bestIndividual);
        }
    }
    
}


