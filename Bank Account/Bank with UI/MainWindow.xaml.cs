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
        public double kontostand = 0;
        public string benutzername = default;
        public string passwort = default;
        public Bank bank = new Bank();
        public MainWindow()
        {

            InitializeComponent();
            viewModel.LoadData();
            
        }

        private void Anmelden(object sender, RoutedEventArgs e)
        {
            viewModel.VMAnmelden(Benutzername.Text, Passwort.Password);
        }

        private void Registrieren(object sender, RoutedEventArgs e)
        {
            benutzername = Benutzername.Text;
            passwort = Passwort.Password;
            if (benutzername != "" && passwort != "")
            {
                if (bank.ExistAccount(benutzername, passwort))
                {
                    MessageBox.Show("Account existiert bereits");
                    bank.SaveData();
                }
                else
                {
                    bank.AccountHinzu(0, benutzername, passwort);
                    viewModel.ConsoleUpdate = $"Sie sind nun Registriert \nWillkommen {benutzername}";
                    viewModel.IsntLoggedIn = true;
                    viewModel.IsLoggedIn = false;

                    bank.SaveData();
                }
            }
            else
            {
                MessageBox.Show("Bitte geben sie ein Passwort und Benutzernamen ein", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void KontostandAnzeigen(object sender, RoutedEventArgs e)
        {
            var kontostandcheck = bank.Kontostand(benutzername, passwort);
            viewModel.Kontostand = $"Ihr Kontostand: {kontostandcheck.Value} Euro";
            kontostand = kontostandcheck.Value;
            bank.SaveData();
        }

        private void Einzahlen(object sender, RoutedEventArgs e)
        {
            double einzahlenMenge = viewModel.EinzahlenFeld;
            if (bank.Einzahlen(einzahlenMenge, benutzername, passwort))
                viewModel.ConsoleUpdate = "Einzahlen erfolgreich.";
            else
                MessageBox.Show("Einzahlen fehlgeschlagen.");
            bank.SaveData();
        }

        private void Abheben(object sender, RoutedEventArgs e)
        {
            double abhebenMenge = viewModel.AbhebenFeld;
            if (bank.Abheben(abhebenMenge, benutzername, passwort))
                viewModel.ConsoleUpdate = "Abhebung erfolgreich.";
            else
                MessageBox.Show("Abhebung fehlgeschlagen. Sie haben das Tageslimit erreicht oder keine valide Zahl eingegeben.");
            bank.SaveData();
        }
        private void Löschen(object sender, RoutedEventArgs e)
        {
            var Result = MessageBox.Show("Bist du dir sicher das du dein Konto Löschen möchtest?", "Konto Löschen", MessageBoxButton.YesNo, MessageBoxImage.Error);
            if (Result == MessageBoxResult.Yes)
            {
                var kontostandcheck = bank.Kontostand(benutzername, passwort);
                if (kontostandcheck.Value == 0)
                {
                    MessageBox.Show("Dein Konto würde gelöscht");
                    viewModel.ConsoleUpdate = "Dein Konto würde gelöscht";
                    bank.AccountEntf(benutzername, passwort);
                    Benutzername.Text = "";
                    Passwort.Password = "";
                    viewModel.IsntLoggedIn = false;
                    viewModel.IsLoggedIn = true;

                    bank.SaveData();
                }
                else
                {
                    MessageBox.Show("Sie haben noch Geld/Schulden auf Ihrem Konto | Vorgang abgebrochen", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    viewModel.ConsoleUpdate = "Die Aktion wurde abgeborchen";
                }

            }
            else if (Result == MessageBoxResult.No)
            {
                MessageBox.Show("Die Aktion wurde abgebrochen");
                viewModel.ConsoleUpdate = "Die Aktion wurde abgeborchen";
            }
        }
        private void Abmelden(object sender, RoutedEventArgs e)
        {
            Benutzername.Text = "";
            Passwort.Password = "";
            viewModel.IsntLoggedIn = false;
            viewModel.IsLoggedIn = true;

            bank.SaveData();
        }
        private void ZahlungsHistory(object sender, RoutedEventArgs e)
        {
            DG_transaction.Visibility = Visibility.Visible;
            viewModel.TransactionsOc = bank.AbrufZahlungsHistorie(benutzername, passwort);


            //var history = bank.AbrufZahlungsHistorie(benutzername, passwort);
            //if (history != null && history.Count > 0)
            //{
            //    string filePath = @$".\{Benutzername.Text}_History.txt";

            //    using (StreamWriter writer = new StreamWriter(filePath))
            //        foreach (var transaction in history)
            //        {
            //            writer.WriteLine($"{Benutzername.Text}: {transaction}");
            //        }
            //    MessageBox.Show("Die History befindent sich in History.txt");
            //    //viewModel.ConsoleUpdate = "";
            //    //foreach (var transaction in history)
            //    //{
            //    //    viewModel.ConsoleUpdate += transaction;
            //    //}
            //}
            //else
            //{
            //    MessageBox.Show("Keine Transaktionen gefunden.");
            //}

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