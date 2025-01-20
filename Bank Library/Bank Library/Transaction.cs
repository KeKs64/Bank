namespace Bank_Library
{
    public class Transaction
    {
        public DateTime Datum { get; }
        public double Betrag { get; set; }
        public string Typ { get; }

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
