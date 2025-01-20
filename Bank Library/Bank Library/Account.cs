using System.Collections.ObjectModel;

namespace Bank_Library
{
    public class Account
    {
        public double Kontostand { get; private set; }
        public string Benutzername { get; private set; }
        private string Passwort;

        private double TagesLimit = 2000;
        public double TagesLimitInsgesamt = 0;
        private DateTime LetzteAbhebung;

        private ObservableCollection<Transaction> ZahlungsHistorie = new ObservableCollection<Transaction>();

        public Account(double kontostand, string benutzername, string passwort)
        {
            Kontostand = kontostand;
            Benutzername = benutzername;
            Passwort = passwort;
            LetzteAbhebung = DateTime.MinValue;
        }
        public bool PasswortÜberprüfen(string passwort)
        {
            return Passwort == passwort;
        }
        public bool Einzahlung(double betrag)
        {
            if (betrag <= 0)
                return false;

            Kontostand += betrag;
            ZahlungsHistorie.Add(new Transaction(DateTime.Now, betrag, "Einzahlung"));
            return true;
        }
        public bool Abhebung(double betrag)
        {
            if (betrag <= 0 || Kontostand - betrag < -100)
                return false;

            if (LetzteAbhebung.Date != DateTime.Now.Date)
            {
                TagesLimitInsgesamt = 0;
                LetzteAbhebung = DateTime.Now;
            }

            if (TagesLimitInsgesamt + betrag > TagesLimit)
                return false;

            Kontostand -= betrag;
            TagesLimitInsgesamt += betrag;
            ZahlungsHistorie.Add(new Transaction(DateTime.Now, betrag, "Abhebung"));
            return true;
        }
        public string Passwortpub()
        {
            return Passwort;
        }
        public ObservableCollection<Transaction> GetZahlungsHistorie()
        {
            return ZahlungsHistorie;
        }
    }
}
