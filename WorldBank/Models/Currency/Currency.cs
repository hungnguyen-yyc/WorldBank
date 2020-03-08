namespace WorldBank.Models.Currency {
    using System;
    public class Currency : ICurrency
    {
        private CurrencyEnum _name;

        public Currency(CurrencyEnum name) {
            this._name = name;
        }

        public CurrencyEnum Name  => this._name;

        public decimal ToCanadianDollars(decimal amount)
        {
            return amount * CurrencyRate.GetRateForCurrency(this.Name);
        }
    }
}