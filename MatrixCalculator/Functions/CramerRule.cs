using System;
using System.Windows;
using MatrixCalculator.Objects;

namespace MatrixCalculator.Functions {
    public class CramerRule {
        public Vector<double> GetCramerRule(Matrix body, Vector<double> answers, Vector<double> variables) {
            try {
                var matrix = new Matrix((double[,])body.Body.Clone()); // Создайтся обьект матрицы с телом переданной матрицы 
                var firstDeterminant = body.GetDeterminant(); // Сохраняется определитель полученной матрицы
                if (firstDeterminant == 0) return null; // Если опред. равен 0, то возвращаем НУЛЛ

                for (var i = 0; i < matrix.GetSize(0); i++) { // Проходимся по матрице
                    matrix.SetColumn(answers, i); // Заменяем колонку на позиции i на колонку ответов в уравнениях
                    variables[i] = Math.Round(matrix.GetDeterminant() / firstDeterminant, 2); // Получаем определитель делённый на сохранёный ранее
                    matrix = new Matrix((double[,])body.Body.Clone()); // Заного копируем значения из эталонной матрицы для послед. преобразования
                }
                return variables; // Возращаем вектор ответов
            }
            catch (Exception e) { // Ловим ошибку и выводим её с одновременным возвращением нулевого вектора
                MessageBox.Show($"{e}");
                return new Vector<double>(new double[] {0,0,0});
            }
        }
    }
}