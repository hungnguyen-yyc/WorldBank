using System;
using WorldBank.Models.Currency;
using WorldBank.Models.User;

namespace WorldBank.Models.Transaction
{
    public interface ITransaction: IEntity
    {
        public DateTime TransactionDate { get; }
        public Account FromAccount { get; }
        public Account ToAccount { get; }
        public decimal Amount { get; }
        public CurrencyEnum Currency { get; }
    }
}
