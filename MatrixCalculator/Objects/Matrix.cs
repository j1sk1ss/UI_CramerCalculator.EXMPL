using System;
using System.Windows;

namespace MatrixCalculator.Objects {
    public class Matrix {
        public Matrix(double[,] body) {
            Body = body;
        }
        public double[,] Body { get; set; }
        
        public double GetElement(int x, int y) => Body[x, y];

        public int GetSize(int dimension) => Body.GetLength(dimension);
        
        public Vector<double> GetRow(int column) {
            var temp = new double[Body.GetLength(1)];
            for (var i = 0; i < temp.Length; i++) temp[i] = Body[i, column];
            return new Vector<double>(temp);
        }
        
        public Vector<double> GetColum(int row) {
            var temp = new double[Body.GetLength(0)];
            for (var i = 0; i < temp.Length; i++) temp[i] = Body[row, i];
            return new Vector<double>(temp);
        }

        public void SetColumn(Vector<double> column, int row) {
            if (column.Size() > Body.GetLength(1)) {
                MessageBox.Show("Error!");
                return;
            }

            for (var i = 0; i < column.Size(); i++) Body[row, i] = column[i];
        }
        
        public void SetRow(Vector<double> row, int column) {
            if (row.Size() > Body.GetLength(0)) {
                MessageBox.Show("Error!");
                return;
            }

            for (var i = 0; i < row.Size(); i++) Body[i, column] = row[i];
        }
        
        public string Print() {
            var temp = "";
            for (var i = 0; i < Body.GetLength(0); i++) {
                for (var j = 0; j < Body.GetLength(1); j++) {
                    temp += Body[i, j];
                }

                temp += "\n";
            }
            return temp;
        }

        public double GetDeterminant() {
            var n = Body.GetLength(1);
            var det = 1d;
            
            var tempBody = Body;
            
            for (var i = 0; i < n; i++) {
                var min = i;
                
                for (var j = i + 1; j < n; ++j) {
                    if (!(tempBody[j, i] < tempBody[min, i]) || !(Math.Abs(tempBody[j, i]) > 0)) continue;
                    min = j;
                    if (i != min) {
                        det *= -1;
                    }
                }
                
                for (var j = 0; j < n; j++) {
                    (tempBody[i, j], tempBody[min, j]) = (tempBody[min, j], tempBody[i, j]);
                    det *= -1;
                }
            }
            
            for (var i = 0; i < n; i++) {
                for (var j = i + 1; j < n; j++) {
                    var flag = 0;
                    if (tempBody[i, i] == 0) {
                        for (var e = i; e < n; e++) {
                            if (tempBody[e, i] != 0) {
                                (tempBody[i, i], tempBody[i, e]) = (tempBody[i, e], tempBody[i, i]);
                            }
                            else {
                                if (++flag == n - i) {
                                    return 0;
                                }
                            }
                        }
                        det *= -1;
                    }
 
                    var coefficient = tempBody[j, i] / tempBody[i, i];
                    for (var k = i; k < n; k++) {
                        tempBody[j, k] -= tempBody[i, k] * coefficient;
                    }
                }
            }
            
            for (var i = 0; i < n; i++) {
                det *= tempBody[i, i];
            }
            
            return det;
        }
    }
}