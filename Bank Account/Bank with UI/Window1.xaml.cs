using Bank_Library;
using System.Collections.ObjectModel;
using System.Windows;

namespace Bank_with_UI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public double laufzeitJahre = 1;
        public double endKapital = 0;
        public double startKapital = 0;
        public double zinsen = 0;
        public double ergebnis = 0;
        public Window1()
        {
            InitializeComponent();
            viewModel1.VisibleJahre = Visibility.Collapsed;
            viewModel1.VisibleEnd = Visibility.Visible;
        }

        private void FesteLaufzeit(object sender, RoutedEventArgs e)
        {
            viewModel1.VisibleJahre = Visibility.Visible;
            viewModel1.VisibleEnd = Visibility.Collapsed;
        }

        private void FestesEndKapital(object sender, RoutedEventArgs e)
        {
            viewModel1.VisibleEnd = Visibility.Visible;
            viewModel1.VisibleJahre = Visibility.Collapsed;
        }

        private void Ergebnis_Jahre(object sender, RoutedEventArgs e)
        {
            viewModel1.VMErgebnisJahre();
        }
        private void Ergebnis_EndKap(object sender, RoutedEventArgs e)
        {
            viewModel1.VMErgebnisEndKap();
        }
    }
}
