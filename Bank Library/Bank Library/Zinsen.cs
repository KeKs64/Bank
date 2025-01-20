namespace Bank_Library
{
    public class Zinsen
    {
        public int Jahr { get; set; }
        public double VerzinstesKapital { get; set; }
        public double KapitalzuwachsEuro { get; set; }
        public double KapitalzuwachsProzent { get; set; }

        public Zinsen(int jahr, double verzinstesKapital, double kapitalzuwachsEuro, double kapitalzuwachsProzent)
        {
            Jahr = jahr;
            VerzinstesKapital = verzinstesKapital;
            KapitalzuwachsEuro = kapitalzuwachsEuro;
            KapitalzuwachsProzent = kapitalzuwachsProzent;
        }
    }
}
