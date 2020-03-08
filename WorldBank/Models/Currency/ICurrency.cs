namespace WorldBank.Models.Currency
{
    public interface ICurrency
    {
        public CurrencyEnum Name { get; }
        public decimal ToCanadianDollars(decimal amount);
    }
}
