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
        }

        private void FesteLaufzeit(object sender, RoutedEventArgs e)
        {
            LaufzeitBlock.Visibility = Visibility.Visible;
            LaufzeitBlockJahre.Visibility = Visibility.Visible;
            LaufzeitBlockStart.Visibility = Visibility.Visible;
            LaufzeitBlockZins.Visibility = Visibility.Visible;

            LaufzeitJahreBox.Visibility = Visibility.Visible;
            LaufzeitStartBox.Visibility = Visibility.Visible;
            LaufzeitZinsBox.Visibility = Visibility.Visible;

            btn_Ergebnis_Jahre.Visibility = Visibility.Visible;

            EndKapBlock.Visibility = Visibility.Collapsed;
            EndKapBlockZiel.Visibility = Visibility.Collapsed;
            EndKapBlockStart.Visibility = Visibility.Collapsed;
            EndKapBlockZins.Visibility = Visibility.Collapsed;

            EndKapZielBox.Visibility = Visibility.Collapsed;
            EndKapStartBox.Visibility = Visibility.Collapsed;
            EndKapZinsBox.Visibility = Visibility.Collapsed;

            btn_Ergebnis_EndKap.Visibility = Visibility.Collapsed;
        }

        private void FestesEndKapital(object sender, RoutedEventArgs e)
        {
            EndKapBlock.Visibility = Visibility.Visible;
            EndKapBlockZiel.Visibility = Visibility.Visible;
            EndKapBlockStart.Visibility = Visibility.Visible;
            EndKapBlockZins.Visibility = Visibility.Visible;

            EndKapZielBox.Visibility = Visibility.Visible;
            EndKapStartBox.Visibility = Visibility.Visible;
            EndKapZinsBox.Visibility = Visibility.Visible;

            btn_Ergebnis_EndKap.Visibility = Visibility.Visible;

            LaufzeitBlock.Visibility = Visibility.Collapsed;
            LaufzeitJahreBox.Visibility = Visibility.Collapsed;
            LaufzeitStartBox.Visibility = Visibility.Collapsed;
            LaufzeitZinsBox.Visibility = Visibility.Collapsed;

            LaufzeitBlockJahre.Visibility = Visibility.Collapsed;
            LaufzeitBlockStart.Visibility = Visibility.Collapsed;
            LaufzeitBlockZins.Visibility = Visibility.Collapsed;

            btn_Ergebnis_Jahre.Visibility = Visibility.Collapsed;
        }

        private void Ergebnis_Jahre(object sender, RoutedEventArgs e)
        {
            double laufzeitJahre = viewModel.LaufzeitJahre;
            double startKapital = viewModel.StartKapital;
            double zinsen = viewModel.Zinsen / 100.0;


            double aktuellesKapital = startKapital;
            var zinsenListe = new ObservableCollection<Zinsen>();

            for (int jahr = 1; jahr <= laufzeitJahre; jahr++)
            {

                double kapitalzuwachsEuro = aktuellesKapital * zinsen;
                aktuellesKapital += kapitalzuwachsEuro;


                double kapitalzuwachsProzent = kapitalzuwachsEuro / startKapital * 100;


                zinsenListe.Add(new Zinsen(
                    jahr,
                    aktuellesKapital,
                    kapitalzuwachsEuro,
                    kapitalzuwachsProzent
                ));
            }


            viewModel.ZinsenOc = zinsenListe;
        }
        private void Ergebnis_EndKap(object sender, RoutedEventArgs e)
        {
            double kapital = viewModel.StartKapital;
            double zinsen = viewModel.Zinsen / 100;
            double zielKapital = viewModel.EndKapital;
            int jahr = 0;

            var zinsenListe = new ObservableCollection<Zinsen>();

            while (kapital < zielKapital)
            {
                jahr++;
                double zinsBetrag = kapital * zinsen;
                kapital += zinsBetrag;


                zinsenListe.Add(new Zinsen(
                    jahr,
                    kapital,
                    zinsBetrag,
                    zinsBetrag / viewModel.StartKapital * 100
                ));
            }
            viewModel.ZinsenOc = zinsenListe;
        }
    }
}
