using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.Model
{
    class Mutation
    {
        protected Random m_random = new Random();
        int m_positionOfMaxBit;
        int m_dimentionsNumber;
        double m_percent;

        public Mutation(int positionOfMaxBit, int dimentionsNumber, double percent)
        {
            m_positionOfMaxBit = positionOfMaxBit;
            m_dimentionsNumber = dimentionsNumber;

            if (percent < 0 || percent > 1)
                throw new ArgumentException("Incorrect percent");

            m_percent = percent;
        }

        public void DoMutation(List<VariablesVector> variablesVectors)
        {
            VariablesVector[] selectedVectors = SelectVectorsForMutation(variablesVectors);

            //debug
            debug_outputLengthOfSelectedVectors(selectedVectors.Length);

            for (int i = 0; i < selectedVectors.Length; i++)
                ChangeVector(selectedVectors[i]);
        }

        private void ChangeVector(VariablesVector variablesVector)
        {
            variablesVector.IsActualF = false;

            int[] variables = variablesVector.Variables;

            for (int i = 0; i < m_dimentionsNumber; i++)
                ChangeVariable(ref variables[i]);
        }

        protected virtual void ChangeVariable(ref int v)
        {
            int bitPosition = m_random.Next(m_positionOfMaxBit);
            int temp = (int)Math.Pow(2, bitPosition);

            v = v ^ temp;
        }

        private VariablesVector[] SelectVectorsForMutation(List<VariablesVector> variablesVectors)
        {
            List<VariablesVector> result = new List<VariablesVector>();

            for (int i = 0; i < variablesVectors.Count; i++)
                if (m_random.NextDouble() <= m_percent)
                    result.Add(variablesVectors[i]);

            return result.ToArray();
        }


        protected virtual void debug_outputLengthOfSelectedVectors(int Vectorslength)
        {
            System.Diagnostics.Debug.WriteLine("Mutation elements = " + Vectorslength);
        }

    }
}
