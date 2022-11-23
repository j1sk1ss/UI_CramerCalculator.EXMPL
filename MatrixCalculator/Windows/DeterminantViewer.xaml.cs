using System.Windows;
using MatrixCalculator.Objects;

namespace MatrixCalculator.Windows
{
    public partial class DeterminantViewer : Window
    {
        public DeterminantViewer(Matrix matrix)
        {
            InitializeComponent();
            Determenant.Content = matrix.Work;
        }
    }
}