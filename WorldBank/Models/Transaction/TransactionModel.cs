using System;
using WorldBank.Models.Currency;
using WorldBank.Models.User;

namespace WorldBank.Models.Transaction
{
    public class TransactionModel : Entity, ITransaction
    {
        private DateTime _transactionDate;
        private Account _fromAccount;
        private Account _toAccounnt;
        private decimal _amount;
        private CurrencyEnum _currency;

        public TransactionModel(Guid id, DateTime transactionDate, Account fromAccount, Account toAccount, decimal amount, CurrencyEnum currency): base(id) {
            this._transactionDate = transactionDate;
            this._fromAccount = fromAccount;
            this._toAccounnt = toAccount;
            this._amount = amount;
            this._currency = currency;
        }

        public DateTime TransactionDate => this._transactionDate;

        public Account FromAccount => this._fromAccount;

        public Account ToAccount => this._toAccounnt;

        public decimal Amount => this._amount;

        public CurrencyEnum Currency => this._currency;
    }
}
