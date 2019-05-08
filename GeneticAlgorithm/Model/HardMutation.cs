using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.Model
{
    class HardMutation:Mutation
    {
        int m_maxValue;

        public HardMutation(int positionOfMaxBit, int dimentionsNumber, double percent)
            :base(positionOfMaxBit, dimentionsNumber, percent)
        {
            m_maxValue = (int)Math.Pow(2, positionOfMaxBit);
        }

        protected override void ChangeVariable(ref int v)
        {
            int temp = m_random.Next(m_maxValue);
            v = v ^ temp;
        }

        protected override void debug_outputLengthOfSelectedVectors(int Vectorslength)
        {
            System.Diagnostics.Debug.WriteLine("HARD Mutation elements = " + Vectorslength);
        }
    }
}
