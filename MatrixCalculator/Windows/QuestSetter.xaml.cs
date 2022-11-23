using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MatrixCalculator.Objects;

namespace MatrixCalculator.Windows {
    public partial class QuestSetter : Window {
        public QuestSetter(MainWindow mainWindow) {
            InitializeComponent();
            Equations = new List<Grid>();
            MainWindow = mainWindow;
        }
        private MainWindow MainWindow { get; set; }
        private List<Grid> Equations { get; set; }

        private readonly List<char> _variableNames = new List<char>() {
            'X', 'Y', 'Z', 'K', 'B', 'P', 'M', 'C', 'F', 'G', 'L', 'U', 'Q', 'V', 'A'
        };
        private int _size;
        
        private void ChangeMatrix() {
            EquationGrid.Children.Clear();
            EquationGrid.Height = 0 + _size * 70;
            for (var i = 0; i < _size; i++) {
                var tempGrid = new Grid {
                    Name = $"Equation{i}",
                    Height = 40,
                    VerticalAlignment = VerticalAlignment.Top
                };
                for (var j = 0; j < _size; j++) {
                    tempGrid.Children.Add(new ComboBox() {
                        Name = $"Coefficient{i}{j}",
                        Width = 50,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(5 + 125 * j, 0, 0, 0),
                        Items = { '+', {'-'} }
                    });
                    tempGrid.Children.Add(new TextBox() {
                        Name = $"Pos{i}{j}",
                        Height = 40,
                        Width = 40,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(60 + 125 * j, 0, 0, 0)
                    });
                    tempGrid.Children.Add(new Label() {
                        FontSize = 20,
                        Margin = new Thickness(100 + j * 125,0,0,0),
                        Content = _variableNames[j]
                    });
                }
                tempGrid.Children.Add(new Label() {
                    FontSize = 20,
                    Margin = new Thickness(0 + _size * 125,0,0,0),
                    Content = '='
                });
                tempGrid.Children.Add(new ComboBox() {
                    Width = 50,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(35 + 125 * _size, 0, 0, 0),
                    Items = { '+', {'-'} }
                });
                tempGrid.Children.Add(new TextBox() {
                    Name = $"Ans{i}",
                    Height = 40,
                    Width = 40,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(80 + 125 * _size, 0, 0, 0)
                });
                tempGrid.Margin = new Thickness(0, 65 * i, 0, 0);
                EquationGrid.Children.Add(tempGrid);
            }
            var tempButton = new Button() {
                Width = 25,
                Height = 25,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(130 + 125 * _size, -12, 0,0),
                Content = '+'
            };
            tempButton.Click += IncreaseMatrix;
            EquationGrid.Children.Add(tempButton);
            
            tempButton = new Button() {
                Width = 25,
                Height = 25,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(130 + 125 * _size, 44, 0,0),
                Content = '-'
            };
            tempButton.Click += DecreaseMatrix;
            EquationGrid.Children.Add(tempButton);
        }
        private void SetValues(object sender, EventArgs e) {
            var tempMatrix = new double[_size, _size];
            var tempAnswers = new double[_size];
            for (var i = 0; i < _size; i++) {
                var tempGrid = EquationGrid.Children[i] as Grid;
                try {
                    var temp = (tempGrid!.Children[^1] as TextBox)!.Text;
                    var tempCof = (tempGrid!.Children[^2] as ComboBox)!.Text;
                    tempAnswers[i] = int.Parse(temp) * (tempCof == "+" ? 1 : -1);
                }
                catch (Exception exception) {
                    MessageBox.Show($"{exception}");
                    return;
                }
                for (var j = 0; j < _size; j++) {
                    try {
                        var tempCombo = tempGrid!.Children[j * 3] as ComboBox;
                        var tempText = tempGrid!.Children[j * 3 + 1] as TextBox;
                        var cof = tempCombo!.Text == "-" ? -1 : 1;
                    
                        tempMatrix[i, j] = cof * int.Parse(tempText!.Text);
                    }
                    catch (Exception exception) {
                        MessageBox.Show($"{exception}");
                        return;
                    }
                }
            } 
            MainWindow.Matrix = new Matrix(tempMatrix);
            MainWindow.Answers = new Vector<double>(tempAnswers);
        }

        private void IncreaseMatrix(object sender, RoutedEventArgs e) {
           _size++; 
           ChangeMatrix();
        }
        private void DecreaseMatrix(object sender, RoutedEventArgs e) {
            if (_size > 1) _size--; 
            ChangeMatrix();
        }
    }
}