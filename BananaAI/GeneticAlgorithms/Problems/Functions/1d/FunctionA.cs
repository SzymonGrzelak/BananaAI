using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace QIGA.Problems
{
    public class FunctionA : IProblem<BitArray, float>
    {
        private float min;
        private float max;

        public FunctionA(float min, float max)
        {
            this.min = min;
            this.max = max;
        }

        public float Evaluate(BitArray individual)
        {
            float x = Getx(individual, this.min, this.max);
            return Value(x);
        }

        private float Value(float x) => (1.5f * MathF.Cos(2.0f * (x - 1.5f)) + 2.2f * MathF.Cos(5.0f * (x - 1.5f))) * MathF.Exp(-MathF.Pow((x - 1.5f) / 5.0f, 2));
        //private float Value(float x) => (x);

        public float Getx(BitArray individual, float min, float max)
        {
            float x = Bin2dec(individual);
            x = min + x * (max - min) / (MathF.Pow(2, individual.Count) - 1);
            return x;
        }

        private int Bin2dec(BitArray bitArray)
        {
            var result = new int[1];
            bitArray.CopyTo(result, 0);
            return result[0];
        }


    }
}
