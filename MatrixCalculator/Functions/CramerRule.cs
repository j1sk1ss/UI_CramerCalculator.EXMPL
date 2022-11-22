using MatrixCalculator.Objects;

namespace MatrixCalculator.Functions
{
    public class CramerRule
    {
        public int GetCramerRule(Matrix body, Vector<double> answers, Vector<double> variables) {
            var matrix = new Matrix(body.Body);
            var firstDeterminant = body.GetDeterminant();
            if (firstDeterminant == 0) return 1;
 
            for (var i = 0; i < matrix.GetSize(0); i++) {
                matrix.SetRow(answers, i);
                variables[i] = matrix.GetDeterminant() / firstDeterminant;
            }
            return 0;
        }
    }
}