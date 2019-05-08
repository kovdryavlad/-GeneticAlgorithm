using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.Model
{
    class GeneticAlgorithm
    {
        int m_iterationsLimit = 10;                        //раничение по итерациям
        int m_populationLength = 100;                      //длинна популяции
        int m_variablesVectorLength = 3;                   //кол-во переменных
        int m_maxValue = 64;                               //верхняя граница переменных 
        double m_bestPersonsPart = 0.02;                   //процент лучших, который оставляем
        double m_mutationPercent = 0.15;                   //процент элементов для мутации
        double m_hardMutationPercent = 0.45;               //процент элементов для катаклизма
        int m_hardMutaionFriquency = 100;                  //через сколько итераций происходит катаклизм
        Func<VariablesVector, double> m_tagretFunction;    //целевая ф-я
        Action<VariablesVector> m_validationFunction;      //функция валидации особи

        public event EventHandler<GeneticAlgorithmEventArgs> processing;
        
        public void SetValidationFunction(Action<VariablesVector> validationFunction)
        {
            m_validationFunction = validationFunction;
        }

        public int Iterations { set { m_iterationsLimit = value; } }

        public int PopulationLength { set { m_populationLength = value; } }

        public GeneticAlgorithm(Func<VariablesVector, double> tagretFunction)
        {
            this.m_tagretFunction = tagretFunction;
        }

        public double Maximize()
        {
            int positionOfMaxBit = Service.getPositionOfMaxBit(m_maxValue);

            Cross crossObj = new Cross(positionOfMaxBit, m_variablesVectorLength);
            Mutation mutationObj = new Mutation(positionOfMaxBit, m_variablesVectorLength, m_mutationPercent);
            HardMutation hardMutationObj = new HardMutation(positionOfMaxBit, m_variablesVectorLength, m_hardMutationPercent);

            List<VariablesVector> population = createFirtsPopulation().ToList();
            CalcFValueForPopulation(population);

            population = population.OrderByDescending(el => el.F).ToList();

            processing?.Invoke(this, new GeneticAlgorithmEventArgs(-1, population[0].F, population.Average(el=>el.F)));

            int bestPersonsLength = Math.Max(1, (int)(m_populationLength * m_bestPersonsPart));

            for (int iterations = 0; iterations < m_iterationsLimit; iterations++)
            {
                //form the roulette
                RouletteElement[] rouletteElements = Roulette.formRouletteElements(population);
                
                //crossing
                for (int i = 0; i < m_populationLength/8; i++)
                {
                    VariablesVector[] selectedByRoulette = Roulette.Twist(rouletteElements.ToList());
                
                    VariablesVector[] descendants = crossObj.TwoPointCrossing(selectedByRoulette);
                    population.AddRange(descendants);
                }

                population = population.OrderByDescending(el => el.F).ToList();

                List<VariablesVector> populationWithoutBestPersons = population.Skip(bestPersonsLength).ToList();

                //and mutation
                mutationObj.DoMutation(populationWithoutBestPersons);

                //hard Mutation
                if (iterations > 0 && iterations % m_hardMutaionFriquency== 0)
                    hardMutationObj.DoMutation(populationWithoutBestPersons);

                CalcFValueForPopulation(population.Where(el => !el.IsActualF).ToList());

                //leaving the best variants
                population = population.OrderByDescending(el => el.F).Take(m_populationLength).ToList();

                //debug
                double F = population. Max(el => el.F);

                processing?.Invoke(this, new GeneticAlgorithmEventArgs(iterations, F, population.Average(el => el.F)));
            }

            double max = population.Max(el => el.F);

            VariablesVector withMax = population.Find(el => el.F == max);
            
            return max;
        }

        VariablesVector[] createFirtsPopulation()
        {
            Random random = new Random();
            VariablesVector[] result = new VariablesVector[m_populationLength];

            for (int i = 0; i < m_populationLength; i++)
            {
                VariablesVector person = new VariablesVector(m_variablesVectorLength);
                person.RandomizeVariables(random, m_maxValue);
                result[i] = person;
            }

            return result;
        }

        void CalcFValueForPopulation(List<VariablesVector> population)
        {
            foreach (VariablesVector person in population)
            {
                m_validationFunction?.Invoke(person);

                double f = m_tagretFunction(person);
                person.SetF(f);
            }
        }
    }
}
