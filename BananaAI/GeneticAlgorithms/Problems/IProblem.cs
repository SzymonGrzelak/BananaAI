using System;
using System.Collections.Generic;
using System.Text;

namespace QIGA.Problems
{
    public interface IProblem<ARGTYPE, RESTYPE>
    {
        public RESTYPE Evaluate(ARGTYPE individual);
        public virtual void Repair(ARGTYPE individual) { }
    }
}
