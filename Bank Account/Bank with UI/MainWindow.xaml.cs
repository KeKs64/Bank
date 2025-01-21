using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Bank_Library;

namespace Bank_with_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            viewModel.LoadData();
        }

        private void Anmelden(object sender, RoutedEventArgs e)
        {
            viewModel.VMAnmelden(Benutzername.Text, Passwort.Text);
        }

        private void Registrieren(object sender, RoutedEventArgs e)
        {
            viewModel.VMRegistrieren(Benutzername.Text, Passwort.Text);
        }
        private void KontostandAnzeigen(object sender, RoutedEventArgs e)
        {
            viewModel.VMKontostandAnzeigen(Benutzername.Text, Passwort.Text);
        }

        private void Einzahlen(object sender, RoutedEventArgs e)
        {
            viewModel.VMEinzahlen(Benutzername.Text, Passwort.Text);
        }

        private void Abheben(object sender, RoutedEventArgs e)
        {
            viewModel.VMAbheben(Benutzername.Text, Passwort.Text);
        }
        private void Löschen(object sender, RoutedEventArgs e)
        {
            viewModel.VMLöschen(Benutzername.Text, Passwort.Text);
        }
        private void Abmelden(object sender, RoutedEventArgs e)
        {
            viewModel.VMAbmelden(Benutzername.Text, Passwort.Text);
        }
        private void ZahlungsHistory(object sender, RoutedEventArgs e)
        {
            viewModel.VMZahlungsHistory(Benutzername.Text, Passwort.Text);   
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Zinsen(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1();
            win1.Show();
        }
    }
}