using QIGA.Algorithm;
using QIGA.Problems;
using System;

namespace GeneticAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            FunctionA problem = new FunctionA(-5, 5);
            QIGA2 algorytm = new QIGA2(problem, 20, 20);
            algorytm.Solve(20);
        }
    }
}
