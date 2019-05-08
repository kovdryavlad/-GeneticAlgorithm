using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.Model
{
    class Service
    {
        public static int getPositionOfMaxBit(int value)
        {
            //max Value Must be power of 2
            int pow = 0;

            for (; Math.Pow(2, pow) < int.MaxValue; pow++)
                if (Math.Pow(2, pow) == value)
                    return pow;

            throw new ArgumentException("maxValue is not power of 2");
        }
    }
}
