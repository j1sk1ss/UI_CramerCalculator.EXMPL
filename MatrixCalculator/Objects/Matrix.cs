using System;
using System.Windows;

namespace MatrixCalculator.Objects
{
    public class Matrix
    {
        public Matrix(double[,] body)
        {
            Body = body;
        }

        public double[,] Body { get; set; }
        public double GetElement(int x, int y) => Body[x, y];
        public int GetSize(int dimension) => Body.GetLength(dimension);

        public Vector<double> GetRow(int column)
        {
            var temp = new double[Body.GetLength(1)];
            for (var i = 0; i < temp.Length; i++) temp[i] = Body[i, column];
            return new Vector<double>(temp);
        }

        public Vector<double> GetColumn(int row)
        {
            var temp = new double[Body.GetLength(0)];
            for (var i = 0; i < temp.Length; i++) temp[i] = Body[row, i];
            return new Vector<double>(temp);
        }

        public void SetRow(Vector<double> row, int column)
        {
            if (row.Size() > Body.GetLength(0))
            {
                MessageBox.Show("Error!");
                return;
            }

            for (var i = 0; i < row.Size(); i++) Body[column, i] = row[i];
        }

        public void SetColumn(Vector<double> column, int row)
        {
            if (column.Size() > Body.GetLength(1))
            {
                MessageBox.Show("Error!");
                return;
            }

            for (var i = 0; i < column.Size(); i++) Body[i, row] = column[i];
        }

        public string Print()
        {
            var temp = "";
            for (var i = 0; i < Body.GetLength(0); i++)
            {
                for (var j = 0; j < Body.GetLength(1); j++)
                {
                    temp += Body[i, j] < 0 ? "" : " ";
                    temp += Body[i, j] + " ";
                }

                temp += "\n";
            }

            return temp;
        }
        public string Work { get; set; }
        public double GetDeterminant() {
            var n = Body.GetLength(1);
            var det = 1d;
            Work = "Находим минимальные значения и переносим их:\n";
            var tempBody = new Matrix((double[,])Body.Clone());

            for (var i = 0; i < n; i++) {
                var min = i;
                
                for (var j = i + 1; j < n; ++j) {
                    if (!(tempBody.Body[j, i] < tempBody.Body[min, i]) || !(Math.Abs(tempBody.Body[j, i]) > 0)) continue;
                    min = j;
                    if (i != min) {
                        det *= -1;
                    }
                }
                
                for (var j = 0; j < n; j++) {
                    Work += $"\nШаг {j + 1}) \n{tempBody.Print()} ↓ ↓ ↓ ↓\n"
                    + $"\nМеняем местами {i + 1};{j + 1}-ый элемент с {min + 1};{j + 1}-ым элементом. ";
                            (tempBody.Body[i, j], tempBody.Body[min, j]) = (tempBody.Body[min, j], tempBody.Body[i, j]);
                    Work += $"\n{tempBody.Print()}";
                    det *= -1;
                } // Сместить все мин. элементы в первый столбец
            }

            Work += "С помощью преобразований делаем треугольную матрицу:\n"; 
            for (var i = 0; i < n; i++) {
                for (var j = i + 1; j < n; j++) {
                    var flag = 0;
                    if (tempBody.Body[i, i] == 0) {
                        Work += "Т.к. элемент на главной диагонале равен нулю приобразуем матрицу сл. образом:";
                        for (var e = i; e < n; e++) {
                            Work += $"\nШаг {j}) Меняем местами элемент {i + 1};{i + 1} c {i + 1};{e + 1}\n{tempBody.Print()}";
                            if (tempBody.Body[e, i] != 0) {
                                (tempBody.Body[i, i], tempBody.Body[i, e]) = (tempBody.Body[i, e], tempBody.Body[i, i]);
                            }
                            else {
                                if (++flag == n - i) {
                                    return 0;
                                }
                            }
                        }
                        det *= -1;
                    }
                    
                    var coefficient = tempBody.Body[j, i] / tempBody.Body[i, i];
                    Work += $"| Делим {j};{i}-ый элемент на {i + 1};{i + 1}-ый получая коэфициент который применяется к:\n{tempBody.Print()}\n";
                    for (var k = i; k < n; k++) {
                        Work += $"\nИз {j + 1};{k + 1} элемента вычитается {i + 1};{k + 1}-ый элемент умноженный на коэфициент.";
                        tempBody.Body[j, k] -= tempBody.Body[i, k] * coefficient;
                        Work += $"\n{tempBody.Print()}";
                    }
                }
            }
            
            for (var i = 0; i < n; i++) {
                det *= tempBody.Body[i, i];
            }
            
            return det;
        }
    }
}
