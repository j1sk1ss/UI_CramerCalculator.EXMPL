using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MatrixCalculator.Gui;
using MatrixCalculator.Objects;

namespace MatrixCalculator.Windows {
    public partial class QuestSetter {
        public QuestSetter(MainWindow mainWindow) { // Коструктор принимает в себя ссылку на главный класс
            InitializeComponent();
            MainWindow = mainWindow;
        }
        private MainWindow MainWindow { get; }
        
        private int _size;
        private void ChangeMatrix() { // Изменения отображения уравнений 
            try {
                EquationGrid.Children.Clear();
                EquationGrid.Height = 0 + _size * 70;
                EquationGrid.Children.Add(Equation.GetEquation(_size, this));
            }
            catch (Exception e) {
                MessageBox.Show($"{e}");
            }
        }
        private void SetValues(object sender, EventArgs e) {  // При закрытии формы с уравнениями они отправляются в гл. класс
            var tempMatrix  = new double[_size, _size];
            var tempAnswers = new double[_size];
            
            for (var i = 0; i < _size; i++) {
                var tempGrid     = (EquationGrid.Children[0] as Grid)!.Children[i] as Grid;
                var temp    = (tempGrid!.Children[^1] as TextBox)!.Text;
                var tempCof = (tempGrid!.Children[^2] as ComboBox)!.Text;
                
                if (double.TryParse(temp, out var tempDoubleAnswer)) {
                    tempAnswers[i] = tempDoubleAnswer * (tempCof == "-" ? -1 : 1);
                }
                else {
                    MessageBox.Show($"На {i} строке ответа введено не число!");
                }
                
                for (var j = 0; j < _size; j++) {
                    var tempCombo = tempGrid!.Children[j * 3] as ComboBox;
                    var tempText  = tempGrid!.Children[j * 3 + 1] as TextBox;
                    var cof    = tempCombo!.Text == "-" ? -1 : 1;

                    if (double.TryParse(tempText!.Text, out var tempDoubleMatrix)) {
                        tempMatrix[i, j] = cof * tempDoubleMatrix;
                    }
                    else {
                        MessageBox.Show($"На {i};{j} введено не число!");
                    }
                }
            } 
            
            MainWindow.Matrix  = new Matrix(tempMatrix);
            MainWindow.Answers = new Vector<double>(tempAnswers);
            Close();
        }
        public void IncreaseMatrix(object sender, RoutedEventArgs e) { // Увеличения кол-ва уравнений
            _size++;
            ChangeMatrix();
        }
        public void DecreaseMatrix(object sender, RoutedEventArgs e) { // Уменьшение кол-ва уравнений до 1, после нельзя
            if (_size > 1) _size--;  
            ChangeMatrix();
        }
        private void Clear(object sender, RoutedEventArgs e) => ChangeMatrix();
    }
}