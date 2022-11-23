using System;
using System.Windows;
using System.Windows.Controls;
using MatrixCalculator.Functions;
using MatrixCalculator.Objects;
using MatrixCalculator.Windows;
using Matrix = MatrixCalculator.Objects.Matrix;

namespace MatrixCalculator {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }
        public Matrix Matrix { get; set; } // Матрица уравнений
        public Vector<double> Answers { get; set; } // Вектор секции ответов
        private void SetVariables(object sender, RoutedEventArgs e) {
            new QuestSetter(this).Show(); // При нажатии на кнопку откроется сеттер уравнений
        }
        private void ShowAnswer(object sender, RoutedEventArgs e) {
            AnswerGrid.Children.Clear();
            AnswerGrid.Children.Add(new Label());
            
            var answer = AnswerGrid.Children[0] as Label;
            answer!.Content = "";
            
            var cramerRule = new CramerRule();
            var detOrig = Math.Round(Matrix.GetDeterminant());
            
            answer.Content += $"Записываем первую часть уравнений как матрицу: \n{Matrix.Print()}" +
                              $"Находим её определитель: {detOrig}\n" +
                              "Подставляем вместо каждой колонки колонку ответов:\n";
            var determinants = new double[Matrix.GetSize(0)];
            
            for (var i = 0; i < Matrix.GetSize(0); i++) {
                var tempMatrix = new Matrix((double[,])Matrix.Body.Clone());
                tempMatrix.SetColumn(Answers, i);
                determinants[i] = Math.Round(tempMatrix.GetDeterminant());
                
                answer.Content += $"{tempMatrix.Print()} Определитель: {determinants[i]}\n \n";
                
                var button = new Button() {
                    Name = $"Matrix{i}",
                    Height = 25,
                    Width = 75,
                    Content = "Подробнее",
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(0,20 + (i + 1) * (85 + tempMatrix.GetSize(0)),0,0)
                };
                button.Click += ShowDetermenant;
                AnswerGrid.Children.Add(button);
                
                tempMatrix = new Matrix((double[,])Matrix.Body.Clone());
            }
            
            answer.Content += $"Делим эти определители на определитель оригинальной матрицы: \n";
            for (var i = 0; i < Matrix.GetSize(0); i++) {
                answer.Content += $"| {Math.Round(determinants[i])} / {detOrig} |   ";
            }

            var variables = new Vector<double>(new double[Matrix.GetSize(0)]);
            variables = cramerRule.GetCramerRule(Matrix, Answers, variables);
            answer.Content += $"\n В итоге получается ответы: {variables.PrintLikeRow()}";
        }

        private void ShowDetermenant(object sender, RoutedEventArgs e) {
            var button = sender as Button;
            var matrix = Matrix;
            matrix.SetColumn(Answers, int.Parse(button!.Name.Replace("Matrix", "")));
            var determinantViewer = new DeterminantViewer(matrix);
            determinantViewer.Show();
        }
    }
}