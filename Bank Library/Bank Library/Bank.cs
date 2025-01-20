using System.Collections.ObjectModel;

namespace Bank_Library
{
    public class Bank
    {
        private List<Account> accounts = new List<Account>();
        private ObservableCollection<Zinsen> ZinsenRechnung = new ObservableCollection<Zinsen>();

        private string filePath = @".\File.txt";

        public bool AccountHinzu(double kontostand, string benutzername, string passwort)
        {
            if (accounts.Any(a => a.Benutzername == benutzername))
                return false;

            accounts.Add(new Account(kontostand, benutzername, passwort));
            return true;
        }

        public bool AccountEntf(string benutzername, string passwort)
        {
            var account = FindAccount(benutzername, passwort);
            if (account != null)
            {
                accounts.Remove(account);
                return true;
            }

            return false;
        }

        public bool Einzahlen(double menge, string benutzername, string passwort)
        {
            var account = FindAccount(benutzername, passwort);
            return account?.Einzahlung(menge) ?? false;
        }

        public bool Abheben(double menge, string benutzername, string passwort)
        {
            var account = FindAccount(benutzername, passwort);
            return account?.Abhebung(menge) ?? false;
        }

        public double? Kontostand(string benutzername, string passwort)
        {
            var account = FindAccount(benutzername, passwort);
            return account?.Kontostand;
        }

        public Account FindAccount(string benutzername, string passwort)
        {
            var account = accounts.FirstOrDefault(a => a.Benutzername == benutzername);
            return account?.PasswortÜberprüfen(passwort) == true ? account : null;
        }

        public bool ExistAccount(string benutzername, string passwort)
        {
            var account = FindAccount(benutzername, passwort);
            if (account != null)
                return true;
            else
                return false;
        }

        public void SaveData()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
                foreach (var account in accounts)
                {
                    //var data = (account.Kontostand, account.Benutzername, account.Passwortpub());
                    //string jsonData = JsonConvert.SerializeObject(data);
                    //writer.Write(jsonData);

                    writer.WriteLine(account.Benutzername);
                    writer.WriteLine(account.Passwortpub());
                    writer.WriteLine(account.Kontostand);
                    writer.WriteLine();
                }
        }

        public (double kontostand, string benutzername, string passwort) LoadData()
        {
            if (!File.Exists(filePath))
            {
                AccountHinzu(5000, "admin", "admin");
                AccountHinzu(5000, "dev", "dev");
                return (0, "", "");
            }

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    //string jsonData = reader.ReadToEnd();
                    //var data = JsonConvert.DeserializeObject<(double, string, string)>(jsonData);
                    //return data;

                    //string a = reader.ReadLine();
                    //string b = reader.ReadLine();
                    //double c = Convert.ToDouble(reader.ReadLine());
                    //reader.ReadLine();
                    
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string a = line.Trim();
                        string b = reader.ReadLine()?.Trim();
                        double c = Convert.ToDouble(reader.ReadLine()?.Trim());
                        
                        accounts.Add(new Account(c, a, b));
                        
                        reader.ReadLine();
                    }

                    return (0, "", "");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
                return (0, "", "");
            }
        }
        public ObservableCollection<Transaction> AbrufZahlungsHistorie(string benutzername, string passwort)
        {
            var account = FindAccount(benutzername, passwort);
            return account?.GetZahlungsHistorie();
        }
        
        public void DevAccounts()
        {
            AccountHinzu(50000, "admin", "admin");
            AccountHinzu(50000, "dev", "dev");
        }
        public void ZinsenRechnen(int jahr, double verzinstesKaptial, double kapitalzuwachsEuro, double kapitalzuwachsProzent)
        {
            ZinsenRechnung.Add(new Zinsen(jahr, verzinstesKaptial, kapitalzuwachsEuro, kapitalzuwachsProzent));
        }
    }
}
