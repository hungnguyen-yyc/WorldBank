using System.Collections.Generic;

namespace WorldBank.Models.Currency {
    public static class CurrencyRate {
        private static Dictionary<CurrencyEnum, decimal> _currencyRate = new Dictionary<CurrencyEnum, decimal>();
        
        static CurrencyRate() {
            _currencyRate.Add(CurrencyEnum.CanadianDollar, (decimal)1.00);
            _currencyRate.Add(CurrencyEnum.USDollar, (decimal)2.00);
            _currencyRate.Add(CurrencyEnum.MexicanPeso, (decimal)0.10);
        }


        public static decimal GetRateForCurrency(CurrencyEnum currency) {
            return _currencyRate[currency];
        }
    }
}