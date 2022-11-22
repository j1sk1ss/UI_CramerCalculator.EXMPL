using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MatrixCalculator.Functions;
using MatrixCalculator.Objects;
using Matrix = MatrixCalculator.Objects.Matrix;

namespace MatrixCalculator {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            CramerRule cramerRule = new CramerRule();
            var a = new double[3, 3];

            a[0, 0] = 1;
            a[0, 1] = 2;
            a[0, 2] = 1;
            
            a[1, 0] = 3;
            a[1, 1] = -1;
            a[1, 2] = -1;
            
            a[2, 0] = -2;
            a[2, 1] = 2;
            a[2, 2] = 3;
            Matrix matrix = new Matrix(a);
            double[] b = {-1, -1, 5};
            
            Vector<double> answers = new Vector<double>(b);

            Vector<double> variables = new Vector<double>(new double[3]);
            
            variables = cramerRule.GetCramerRule(matrix, answers, variables);
            
            A.Content = variables[0] + " " + variables[1] + " " + variables[2];
        }
    }
}