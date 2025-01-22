using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Transactions;

namespace Bank_Library
{
    public class Transaction
    {
        public DateTime Datum { get; }
        public double Betrag { get; set; }
        public string Typ { get; }

        [JsonConstructor]
        public Transaction(DateTime datum, double betrag, string typ)
        {
            Datum = datum;
            Betrag = betrag;
            Typ = typ;
        }

        public override string ToString()
        {
            return $"{Datum}: {Typ} von {Betrag:F2} Euro";
        }
    }
}
