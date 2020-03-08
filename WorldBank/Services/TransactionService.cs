using System.Collections.Generic;
using WorldBank.Models.User;
using WorldBank.Models.Transaction;
using WorldBank.Models.Currency;
using System;

namespace WorldBank.Service {
    public class TransactionService {

        public Dictionary<Account, List<TransactionModel>> _accountTransactions;

        public TransactionService() {
            this._accountTransactions = new Dictionary<Account, List<TransactionModel>>();
        }

        public void AddAccount(Account account) {
            this._accountTransactions.Add(account, new List<TransactionModel>());
        }

        public void Transfer(Customer requester, Account fromAccount, Account toAccount, decimal amount, CurrencyEnum currency) {
            TransactionModel transaction = new TransactionModel(Guid.NewGuid(), new DateTime(), fromAccount, toAccount, amount, currency);
            if(this.isValidTransaction(requester, transaction)) {
                this._accountTransactions[transaction.ToAccount].Add(transaction);
                this._accountTransactions[transaction.FromAccount].Add(transaction);
            }
        }

        public decimal GetBalance(Account account) {
            if (this._accountTransactions[account] == null) {
                throw new ArgumentException("Account " + account.Id + " is invalid.");
            }
            List<TransactionModel> transactions = this._accountTransactions[account];
            decimal balance = new Decimal(0);
            foreach (TransactionModel transaction in transactions)
            {
                Currency currency = new Currency(transaction.Currency);
                if (account.Equals(transaction.ToAccount)) {
                    balance = Decimal.Add(balance, currency.ToCanadianDollars(transaction.Amount));
                } 
                else if (account.Equals(transaction.FromAccount)) {
                    balance = Decimal.Subtract(balance, currency.ToCanadianDollars(transaction.Amount));
                }
            }
            return balance;
        }

        private bool isValidTransaction(Customer requester, TransactionModel transaction) {
            if (this._accountTransactions[transaction.FromAccount] == null) {
                return false;
            }
            if (this._accountTransactions[transaction.ToAccount] == null) {
                return false;
            }
            List<Account> accounts = new List<Account>(requester.BankingAccounts);
            accounts.Add(requester.CashAccount);
            foreach (Account account in accounts)
            {
                if(account.Equals(transaction.FromAccount)) {
                    return true;
                }
            }
            return false;
        }
    }
}