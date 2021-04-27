using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QIGA.DataStructures
{
    class Register
    {
        public List<double> Amplitudes{ get; set; } 

        public Register(int registerSize) {
            this.Amplitudes = new List<double>();
            for (int i = 0; i < Math.Pow(2, registerSize); i++) this.Amplitudes.Add(Math.Sqrt(1/ Math.Pow(2, registerSize)));
        }

    }
}
