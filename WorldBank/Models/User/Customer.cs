using System;
using System.Collections.Generic;

namespace WorldBank.Models.User {
    public class Customer : User {

        private List<Account> _bankingAccounts;
        private Account _cashAccount; // default placeholder account, consider this as a wallet

        public Customer(Guid id, string firstName, string lastName, List<Account> accounts, Account cashAccount) : base (id, firstName, lastName) {
            this._bankingAccounts = accounts;
            this._cashAccount = cashAccount;
        }

        public List<Account> BankingAccounts => this._bankingAccounts;
        public Account CashAccount => this._cashAccount;
    }
}