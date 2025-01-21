using Bank_Library;
using System.Collections.ObjectModel;
using System.Windows;

namespace Bank_with_UI;

public class MainViewModel : ObservableObject
{

    private string consoleUpdate = "Um unsere Funktionen freizuschalten bitte Anmelden";
    public string ConsoleUpdate
    {
        get { return consoleUpdate; }
        set
        {
            consoleUpdate = value;
            OnPropertyChanged(nameof(ConsoleUpdate));
        }
    }

    private string kontostand2 = "";
    public string Kontostand
    {
        get { return kontostand2; }
        set
        {
            kontostand2 = value;
            OnPropertyChanged(nameof(kontostand));
        }
    }

    private string währung = "Euro";
    public string Währung
    {
        get { return währung; }
        set
        {
            währung = value;
            OnPropertyChanged(nameof(Währung));
        }
    }

    private double einzahlen;
    public double EinzahlenFeld
    {
        get { return einzahlen; }
        set
        {
            einzahlen = value;
            OnPropertyChanged(nameof(EinzahlenFeld));
        }
    }
    private double abheben;
    public double AbhebenFeld
    {
        get { return abheben; }
        set
        {
            abheben = value;
            OnPropertyChanged(nameof(AbhebenFeld));
        }
    }

    private double laufzeitJahre;
    public double LaufzeitJahre
    {
        get { return laufzeitJahre; }
        set
        {
            laufzeitJahre = value;
            OnPropertyChanged(nameof(LaufzeitJahre));
        }
    }

    private double endKapital;
    public double EndKapital
    {
        get { return endKapital; }
        set
        {
            endKapital = value;
            OnPropertyChanged(nameof(EndKapital));
        }
    }

    private double startKapital;
    public double StartKapital
    {
        get { return startKapital; }
        set
        {
            startKapital = value;
            OnPropertyChanged(nameof(StartKapital));
        }
    }

    private double zinsen;
    public double Zinsen
    {
        get { return zinsen; }
        set
        {
            zinsen = value;
            OnPropertyChanged(nameof(Zinsen));
        }
    }

    private ObservableCollection<Transaction> transactionsOc;
    public ObservableCollection<Transaction> TransactionsOc
    {
        get { return transactionsOc; }
        set
        {
            transactionsOc = value;
            OnPropertyChanged(nameof(TransactionsOc));
        }
    }

    private ObservableCollection<Zinsen> zinsenOc;
    public ObservableCollection<Zinsen> ZinsenOc
    {
        get { return zinsenOc; }
        set
        {
            zinsenOc = value;
            OnPropertyChanged(nameof(ZinsenOc));
        }
    }



    private bool isLoggedIn = true;
    public bool IsLoggedIn
    {
        get { return isLoggedIn; }
        set
        {
            isLoggedIn = value;
            OnPropertyChanged(nameof(IsLoggedIn));
        }
    }
    private bool isntLoggedIn = false;
    public bool IsntLoggedIn
    {
        get { return isntLoggedIn; }
        set
        {
            isntLoggedIn = value;
            OnPropertyChanged(nameof(IsntLoggedIn));
        }
    }
    private Visibility visibleJahre;
    public Visibility VisibleJahre
    {
        get { return visibleJahre; }
        set
        {
            visibleJahre = value;
            OnPropertyChanged(nameof(VisibleJahre));
        }
    }
    private Visibility visibleEnd;
    public Visibility VisibleEnd
    {
        get { return visibleEnd; }
        set
        {
            visibleEnd = value;
            OnPropertyChanged(nameof(VisibleEnd));
        }
    }

    internal void VMErgebnisJahre()
    {
        double laufzeitJahre = LaufzeitJahre;
        double startKapital = StartKapital;
        double zinsen = Zinsen / 100.0;


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


        ZinsenOc = zinsenListe;
    }
    internal void VMErgebnisEndKap()
    {

        double kapital = StartKapital;
        double zinsen = Zinsen / 100;
        double zielKapital = EndKapital;
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
                zinsBetrag / StartKapital * 100
            ));
        }
        ZinsenOc = zinsenListe;
    }

    public double kontostand = 0;
    public string benutzername = default;
    public string passwort = default;
    public Bank bank = new Bank();

    internal void LoadData()
    {
        bank.LoadData();
        bank.SaveData();
    }

    internal void VMAnmelden(string username, string password)
    {
        benutzername = username;
        passwort = password;
        if (bank.ExistAccount(benutzername, passwort))
        {
            ConsoleUpdate = $"Willkommen {benutzername}";
            IsntLoggedIn = true;
            IsLoggedIn = false;

            bank.SaveData();
        }
        else
        {
            MessageBox.Show("Account nicht gefunden");
            bank.SaveData();
        }
    }
}
