using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.Model
{
    class RouletteElement
    {
        public VariablesVector VariablesVector { get; set; }

        public double TopProbability { get; set; }
        public double BottomProbability { get; set; }

        public RouletteElement(VariablesVector variablesVector, double bottomProbability, double topProbability)
        {
            this.VariablesVector = variablesVector;
            this.BottomProbability = bottomProbability;
            this.TopProbability = topProbability;
        }

        public override string ToString()
        {
            return VariablesVector + 
                String.Format(" | Probabity: {0:0.000} - {1:0.000}", BottomProbability, TopProbability);
        }
    }
}
