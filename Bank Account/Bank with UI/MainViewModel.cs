using Bank_Library;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace Bank_with_UI;

public class MainViewModel : ObservableObject
{
    public Bank bank = new Bank();

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

    private Account loginUser;
    public Account LogInUser
    {
        get { return loginUser; }
        set
        {
            loginUser = value;
            OnPropertyChanged(nameof(LogInUser));
        }
    }

    private string benutzername;
    public string Benutzername
    {
        get { return benutzername; }
        set
        {
            benutzername = value;
            OnPropertyChanged(nameof(Benutzername));
        }
    }
    private string password;
    public string Password
    {
        get { return password; }
        set
        {
            password = value;
            OnPropertyChanged(nameof(Password));
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



    internal void LoadData()
    {
        //bank.DevAccounts();
        bank.LoadData();
        bank.SaveData();
    }

    internal void VMAnmelden(string username, string password)
    {
        
        if (bank.ExistAccount(username, password))
        {
            ConsoleUpdate = $"Willkommen {username}";
            IsntLoggedIn = true;
            IsLoggedIn = false;

            bank.SaveData();
            LogInUser = bank.FindAccount(username, password);
        }
        else
        {
            MessageBox.Show("Account nicht gefunden");
            bank.SaveData();
        }

    }
    internal void VMRegistrieren(string username, string password)
    {
        if (username != "" && password != "")
        {
            if (bank.ExistAccount(username, password))
            {
                MessageBox.Show("Account existiert bereits");
                bank.SaveData();
            }
            else
            {
                bank.AccountHinzu(0, username, password);
                ConsoleUpdate = $"Sie sind nun Registriert \nWillkommen {username}";
                IsntLoggedIn = true;
                IsLoggedIn = false;

                bank.SaveData();
                LogInUser = bank.FindAccount(username, password);
            }
        }
        else
        {
            MessageBox.Show("Bitte geben sie ein Passwort und Benutzernamen ein", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    /*internal void VMKontostandAnzeigen(string username, string password)
    //{
        
    //    var kontostandcheck = bank.Kontostand(username, password);
    //    loginUser.Kontostand = kontostandcheck.Value;
    //    bank.SaveData();
    //}
    */
    internal void VMEinzahlen(string username, string password)
    {
        double einzahlenMenge = EinzahlenFeld;
        if (bank.Einzahlen(einzahlenMenge, username, password))
            ConsoleUpdate = "Einzahlen erfolgreich.";
        else
            MessageBox.Show("Einzahlen fehlgeschlagen.");
        bank.SaveData();
    }
    internal void VMAbheben(string username, string password)
    {
        double abhebenMenge = AbhebenFeld;
        if (bank.Abheben(abhebenMenge, username, password))
            ConsoleUpdate = "Abhebung erfolgreich.";
        else
            MessageBox.Show("Abhebung fehlgeschlagen. Sie haben das Tageslimit erreicht oder keine valide Zahl eingegeben.");
        bank.SaveData();
    }
    internal void VMLöschen(string username, string password)
    {
        var Result = MessageBox.Show("Bist du dir sicher das du dein Konto Löschen möchtest?", "Konto Löschen", MessageBoxButton.YesNo, MessageBoxImage.Error);
        if (Result == MessageBoxResult.Yes)
        {
            var kontostandcheck = bank.Kontostand(username, password);
            if (kontostandcheck.Value == 0)
            {
                MessageBox.Show("Dein Konto würde gelöscht");
                ConsoleUpdate = "Dein Konto würde gelöscht";
                bank.AccountEntf(username, password);
                Benutzername = "";
                Password = "";
                IsntLoggedIn = false;
                IsLoggedIn = true;

                bank.SaveData();
            }
            else
            {
                MessageBox.Show("Sie haben noch Geld/Schulden auf Ihrem Konto | Vorgang abgebrochen", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ConsoleUpdate = "Die Aktion wurde abgeborchen";
            }

        }
        else if (Result == MessageBoxResult.No)
        {
            MessageBox.Show("Die Aktion wurde abgebrochen");
            ConsoleUpdate = "Die Aktion wurde abgeborchen";
        }
    }
    internal void VMAbmelden(string username, string password)
    {
        Benutzername = "";
        Password = "";
        IsntLoggedIn = false;
        IsLoggedIn = true;

        bank.SaveData();
        LogInUser = null;
        username = "";
        password = "";
        VMZahlungsHistory(username, password);
    }
    internal void VMZahlungsHistory(string username, string password)
    {
        TransactionsOc = bank.AbrufZahlungsHistorie(username, password);
    }

}
