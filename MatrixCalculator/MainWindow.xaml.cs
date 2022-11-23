using System;
using System.Windows;
using MatrixCalculator.Functions;
using MatrixCalculator.Objects;
using MatrixCalculator.Windows;
using Matrix = MatrixCalculator.Objects.Matrix;

namespace MatrixCalculator {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }
        public Matrix Matrix { get; set; }
        public Vector<double> Answers { get; set; }
        private void SetVariables(object sender, RoutedEventArgs e) {
            new QuestSetter(this).Show();
        }

        private void ShowAnswer(object sender, RoutedEventArgs e)
        {
            A.Content = "";
            var cramerRule = new CramerRule();
            var detOrig = Math.Round(Matrix.GetDeterminant());
            A.Content += $"Записываем первую часть уравнений как матрицу: \n{Matrix.Print()}" +
                        $"Находим её определитель: {detOrig}\n" +
                        $"Подставляем вместо каждой колонки колонку ответов:\n";
            var determinats = new double[Matrix.GetSize(0)];
            
            for (var i = 0; i < Matrix.GetSize(0); i++) {
                var tempMatrix = new Matrix((double[,])Matrix.Body.Clone());
                tempMatrix.SetColumn(Answers, i);
                determinats[i] = Math.Round(tempMatrix.GetDeterminant());
                
                A.Content += $"{tempMatrix.Print()} Определитель: {determinats[i]}\n \n";
                tempMatrix = new Matrix((double[,])Matrix.Body.Clone());
            }
            
            A.Content += $"Делим эти определители на определитель оригинальной матрицы: \n";
            
            for (var i = 0; i < Matrix.GetSize(0); i++) {
                A.Content += $"| {Math.Round(determinats[i])} / {detOrig} |   ";
            }

            var variables = new Vector<double>(new double[Matrix.GetSize(0)]);
            variables = cramerRule.GetCramerRule(Matrix, Answers, variables);
            A.Content += $"\n \n В итоге получается ответы: {variables.PrintLikeRow()}";
            
        }
    }
}