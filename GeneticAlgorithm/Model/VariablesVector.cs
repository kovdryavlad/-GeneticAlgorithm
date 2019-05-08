using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.Model
{
    class VariablesVector : ICloneable
    {
        int[] m_variables;
        int m_variablesLength;
        double m_f;

        public double F { get => m_f; }

        public int[] Variables { get => m_variables; }

        public bool IsActualF { get; set; }

        private VariablesVector() { }

        public VariablesVector(int variablesLength)
        {
            m_variablesLength = variablesLength;
            m_variables = new int[variablesLength];
        }

        public void RandomizeVariables(Random random, int maxValue)
        {
            for (int i = 0; i < m_variablesLength; i++)
                m_variables[i] = random.Next(maxValue);
        }

        public void SetF(double f)
        {
            m_f = f;
            IsActualF = true;
        }

        public override string ToString()
        {
            string res = String.Empty;

            for (int i = 0; i < m_variablesLength; i++)
                res += m_variables[i] + "; ";

            return res + " | F = " + F;
        }

        public object Clone()
        {
            VariablesVector result = new VariablesVector();
            result.m_variables = (int[])m_variables.Clone();
            result.m_variablesLength = m_variablesLength;
            result.m_f = m_f;
            result.IsActualF = IsActualF;

            return result;
        }
    }
}
