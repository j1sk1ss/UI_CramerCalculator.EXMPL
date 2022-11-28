using System;
using System.Windows;

namespace MatrixCalculator.Objects {
    public class Matrix {
        public Matrix(double[,] body) { // Конструктор принимаем двумерную матрицу даблов и заносит их в тело
            Body = body;
        }
        public double[,] Body { get; set; } // Тело
        public double GetElement(int x, int y) => Body[x, y]; // Метод получения элемента по координатам
        public int GetSize(int dimension) => Body.GetLength(dimension); // Метод получения длины одного из измерений
        public Vector<double> GetRow(int column) { // Метод получения строки вектором
            var temp = new double[Body.GetLength(1)]; // Создаётся вектор длиной равной длине строки матрицы 
            for (var i = 0; i < temp.Length; i++) temp[i] = Body[i, column]; // Туда копируются значения
            return new Vector<double>(temp); // Возвращаем вектор
        }
        public Vector<double> GetColumn(int row) { // Метод получения столбца вектором
            var temp = new double[Body.GetLength(0)]; // Создаётся вектор длиной равной длине строки столбца 
            for (var i = 0; i < temp.Length; i++) temp[i] = Body[row, i]; // Туда копируются значения
            return new Vector<double>(temp);  // Возвращаем вектор
        }
        public void SetRow(Vector<double> row, int column) { // Метод установки строки вектором
            if (row.Size() > Body.GetLength(0)) {  // Если полученный вектор больше чем есть в матрице - выводит ошибку
                MessageBox.Show("Error!");
                return;
            }

            for (var i = 0; i < row.Size(); i++) Body[column, i] = row[i]; 
        }
        public void SetColumn(Vector<double> column, int row) { // Метод установки столбца вектором
            if (column.Size() > Body.GetLength(1)) {  // Если полученный вектор больше чем есть в матрице - выводит ошибку
                MessageBox.Show("Error!");
                return;
            }

            for (var i = 0; i < column.Size(); i++) Body[i, row] = column[i];
        }
        public string Print() { // Метод вывода всей матрицы строкой
            var temp = "";
            for (var i = 0; i < Body.GetLength(0); i++) {
                for (var j = 0; j < Body.GetLength(1); j++) {
                    temp += Body[i, j] < 0 ? "" : " ";
                    temp += Body[i, j] + " ";
                }

                temp += "\n";
            }
            return temp;
        }
        public string Work { get; set; } // Строка хранящая в себе процесс нахождения определителя
        public double GetDeterminant() { // Метод нахождения определителя
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
                }
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
            
            if (Math.Abs(Math.Round(det, 0)) == 0) {
                MessageBox.Show($"Определитель матрицы = {Math.Round(det, 0)}\n" +
                                "СЛАУ не имеет решений", "Результат!");
            }

            return det;
        }
    }
}
