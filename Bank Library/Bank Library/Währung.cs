namespace Bank_Library
{
    public class Währung
    {
        public string Euro { get; set; }
        public string Dollar { get; set; }

        public Währung(string euro, string dollar)
        {
            Euro = euro;
            Dollar = dollar;
        }
    }
}
