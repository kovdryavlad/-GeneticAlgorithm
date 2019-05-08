using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.Model
{
    static class Roulette
    {
        public static RouletteElement[] formRouletteElements(List<VariablesVector> population)
        {
            RouletteElement[] result = new RouletteElement[population.Count];

            double sumOfFitnessFunctionOfAllPersnons = population.Sum(el => el.F);
            double accumulativeSumOfProbability = 0;

            for (int i = 0; i < population.Count; i++)
            {
                VariablesVector currentPerson = population[i];

                double probability =currentPerson.F / sumOfFitnessFunctionOfAllPersnons;
                double topProbability = accumulativeSumOfProbability + probability;

                result[i] = new RouletteElement(currentPerson,
                                                accumulativeSumOfProbability,
                                                topProbability);

                accumulativeSumOfProbability = topProbability;
            }

            return result;
        }

        public static VariablesVector[] Twist(List<RouletteElement> rouletteElements)
        {
            Random random = new Random();
            int neededVariablesVectorLength = 2;    //amount of persons
            VariablesVector[] result = new VariablesVector[neededVariablesVectorLength];

            for (int i = 0; i < neededVariablesVectorLength; i++)
            {
                double r = random.NextDouble();
                result[i] = rouletteElements.Find(el => r >= el.BottomProbability && r < el.TopProbability).VariablesVector;
            }

            return result;
        }
    }
}
