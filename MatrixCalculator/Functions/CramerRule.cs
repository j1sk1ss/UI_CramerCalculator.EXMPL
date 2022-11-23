using System;
using System.Windows;
using MatrixCalculator.Objects;

namespace MatrixCalculator.Functions {
    public class CramerRule {
        public Vector<double> GetCramerRule(Matrix body, Vector<double> answers, Vector<double> variables) {
            try {
                var matrix = new Matrix((double[,])body.Body.Clone());
                var firstDeterminant = body.GetDeterminant();
                if (firstDeterminant == 0) return null;

                for (var i = 0; i < matrix.GetSize(0); i++) {
                    matrix.SetColumn(answers, i);
                    variables[i] = Math.Round(matrix.GetDeterminant() / firstDeterminant, 0);
                    matrix = new Matrix((double[,])body.Body.Clone());
                }
                return variables;
            }
            catch (Exception e) {
                MessageBox.Show($"{e}");
                return new Vector<double>(new double[] {0,0,0});
            }
        }
    }
}