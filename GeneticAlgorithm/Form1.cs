using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeneticAlgorithm.Model;

namespace GeneticAlgorithmNS
{
    public partial class Form1 : Form
    {
        GeneticAlgorithm.Model.GeneticAlgorithm geneticAlgorithm;

        public Form1()
        {
            InitializeComponent();

            geneticAlgorithm = new GeneticAlgorithm.Model.GeneticAlgorithm(targetFunction);

            geneticAlgorithm.SetValidationFunction((varsVector) =>
            {
                int[] variables = varsVector.Variables;

                for (int i = 0; i < variables.Length; i++)
                    if (variables[i] == 0)
                        variables[i] = 1;
            });

            geneticAlgorithm.processing += (sender, e) =>
            {
                chart1.Series[0].Points.AddXY(e.Iteration, e.F);
                chart1.Series[1].Points.AddXY(e.Iteration, e.Average);
            };


        }

        Func<VariablesVector, double> targetFunction = (variablesVector) =>
        {
            double x = variablesVector.Variables[0];
            double y = variablesVector.Variables[1];
            double z = variablesVector.Variables[2];

            return 64 / x + x * x / y + y * y / z + z * z / 100;
            //return  x + y + z;
            //return  x;
        };

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chart1.Series.Count; i++)
                chart1.Series[i].Points.Clear();

            geneticAlgorithm.Iterations = Convert.ToInt32(IterTextBox.Text);
            geneticAlgorithm.PopulationLength = Convert.ToInt32(PopulationLengthTextBox.Text);

            double result = geneticAlgorithm.Maximize();
            ResLabel.Text = result.ToString("0.0000");

            double min = chart1.Series[1].Points.Min(el => el.YValues[0]);
            chart1.ChartAreas[0].AxisY.Minimum = min;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0.00}";
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "{0:0.00}";
        }
    }
}
