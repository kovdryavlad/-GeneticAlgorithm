using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.Model
{
    class Cross
    {
        Random m_random = new Random();
        int m_positionOfMaxBit;
        int m_dimentionsNumber;

        public Cross(int positionOfMaxBit, int dimentionsNumber)
        {
            m_positionOfMaxBit = positionOfMaxBit;
            m_dimentionsNumber = dimentionsNumber;
        }

        public VariablesVector[] TwoPointCrossing(VariablesVector[] variablesVectors)
        {
            VariablesVector a = variablesVectors[0];
            VariablesVector b = variablesVectors[1];

            VariablesVector[] result = new[] { new VariablesVector(m_dimentionsNumber), new VariablesVector(m_dimentionsNumber) };

            for (int dimNumber = 0; dimNumber < m_dimentionsNumber; dimNumber++)
            {
                //получение результата скрещивания по одному измерению 
                int[] crossingInDimentionResult = CrossTwoVariables(a.Variables[dimNumber], b.Variables[dimNumber]);

                //переносим это в массив результатов
                AssingCrossingResultToDimention(result, crossingInDimentionResult, dimNumber);
            }

            return result;
        }

        private void AssingCrossingResultToDimention(VariablesVector[] descendants, int[] crossingInDimentionResult, int dimNumber)
        {
            for (int i = 0; i < descendants.Length; i++)
                descendants[i].Variables[dimNumber] = crossingInDimentionResult[i];
        }

        private int[] CrossTwoVariables(int a, int b)
        {
            int[] numbersOfBits = getNumbersOfBits(2);

            int temp = 0;
            for (int i = numbersOfBits[0]; i <= numbersOfBits[1]; i++)
                temp += (int)Math.Pow(2, i);

            int aPart = a & temp;
            int bPart = b & temp;

            int tempNegative = ~temp;

            a = a & tempNegative;
            b = b & tempNegative;

            a = a | bPart;
            b = b | aPart;

            return new[] { a, b };
        }

        private int[] getNumbersOfBits(int length)
        {
            List<int> result = new List<int>();
            Func<int> rand = () => m_random.Next(m_positionOfMaxBit);

            while (result.Count != length)
            {
                int rValue = rand();

                if (!result.Contains(rValue))
                    result.Add(rValue);
            }

            return result.OrderBy(el => el).ToArray();
        }
    }
}
