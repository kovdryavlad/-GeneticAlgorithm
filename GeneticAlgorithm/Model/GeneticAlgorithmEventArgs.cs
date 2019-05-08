using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.Model
{
    class GeneticAlgorithmEventArgs:EventArgs
    {
        int m_iteration;
        double m_maxfValue;
        double m_average;

        public GeneticAlgorithmEventArgs(int iteration, double maxF, double average)
        {
            m_iteration = iteration;
            m_maxfValue = maxF;
            m_average = average;
        }

        public int Iteration { get => m_iteration; }
        public double F { get => m_maxfValue; }
        public double Average { get => m_average; }

    }
}
