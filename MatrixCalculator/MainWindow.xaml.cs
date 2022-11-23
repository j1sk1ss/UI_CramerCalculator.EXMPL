using System.Windows;
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
    }
}