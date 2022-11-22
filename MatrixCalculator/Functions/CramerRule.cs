using System;
using MatrixCalculator.Objects;

namespace MatrixCalculator.Functions {
    public class CramerRule {
        public Vector<double> GetCramerRule(Matrix body, Vector<double> answers, Vector<double> variables) {
            var matrix = new Matrix((double[,])body.Body.Clone());
            var firstDeterminant = body.GetDeterminant();
            if (firstDeterminant == 0) return null;

            for (var i = 0; i < matrix.GetSize(0); i++) {
                matrix.SetRow(answers, i);
                variables[i] = Math.Round(matrix.GetDeterminant() / firstDeterminant, 0);
                matrix = new Matrix((double[,])body.Body.Clone());
            }
            return variables;
        }
    }
}