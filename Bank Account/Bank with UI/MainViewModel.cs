using Bank_Library;
using System.Collections.ObjectModel;
using System.Windows;

namespace Bank_with_UI;

public class MainViewModel : ObservableObject
{

    private string consoleUpdate = "";
    private string kontostand = "";
    private string währung = "Euro";
    private double einzahlen;
    private double abheben;
    private double laufzeitJahre;
    private double endKapital;
    private double startKapital;
    private double zinsen;
    private ObservableCollection<Transaction> transactionsOc;
    private ObservableCollection<Zinsen> zinsenOc;


    public string ConsoleUpdate
    {
        get { return consoleUpdate; }
        set
        {
            consoleUpdate = value;
            OnPropertyChanged(nameof(ConsoleUpdate));
        }
    }
    public string Kontostand
    {
        get { return kontostand; }
        set
        {
            kontostand = value;
            OnPropertyChanged(nameof(kontostand));
        }
    }
    public string Währung
    {
        get { return währung; }
        set
        {
            währung = value;
            OnPropertyChanged(nameof(Währung));
        }
    }
    public double EinzahlenFeld
    {
        get { return einzahlen; }
        set
        {
            einzahlen = value;
            OnPropertyChanged(nameof(EinzahlenFeld));
        }
    }
    public double AbhebenFeld
    {
        get { return abheben; }
        set
        {
            abheben = value;
            OnPropertyChanged(nameof(AbhebenFeld));
        }
    }
    public double LaufzeitJahre
    {
        get { return laufzeitJahre; }
        set
        {
            laufzeitJahre = value;
            OnPropertyChanged(nameof(LaufzeitJahre));
        }
    }
    public double EndKapital
    {
        get { return endKapital; }
        set
        {
            endKapital = value;
            OnPropertyChanged(nameof(EndKapital));
        }
    }
    public double StartKapital
    {
        get { return startKapital; }
        set
        {
            startKapital = value;
            OnPropertyChanged(nameof(StartKapital));
        }
    }
    public double Zinsen
    {
        get { return zinsen; }
        set
        {
            zinsen = value;
            OnPropertyChanged(nameof(Zinsen));
        }
    }
    public ObservableCollection<Transaction> TransactionsOc
    {
        get { return transactionsOc; }
        set
        {
            transactionsOc = value;
            OnPropertyChanged(nameof(TransactionsOc));
        }
    }
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
    private bool visibleJahre = false;
    public bool VisibleJahre
    {
        get { return visibleJahre; }
        set
        {
            visibleJahre = value;
            OnPropertyChanged(nameof(VisibleJahre));
        }
    }
    private bool visibleEnd = false;
    public bool VisibleEnd
    {
        get { return visibleEnd; }
        set
        {
            visibleEnd = value;
            OnPropertyChanged(nameof(VisibleEnd));
        }
    }

}
