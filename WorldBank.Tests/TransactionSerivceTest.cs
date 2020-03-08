using NUnit.Framework;
using WorldBank.Models.User;
using WorldBank.Models.Transaction;
using WorldBank.Models.Currency;
using WorldBank.Service;
using System;
using System.Collections.Generic;

namespace WorldBank.Tests
{
    [TestFixture]
    public class TransactionServiceTest
    {

        private TransactionService _service;

        [SetUp]
        public void Setup()
        {
            this._service = new TransactionService();
        }

        [Test]
        public void ShouldCreateDeposit()
        {
            // case 1: initial balance: 100.00 CAD
            // arrange
            Account account = new Account(Guid.NewGuid());
            Customer customer = this.CreateCustomer("Stewie", "Griffin", new List<Account>(){account});
            this._service.AddAccount(account);
            this._service.AddAccount(customer.CashAccount);
            this._service.Transfer(customer, customer.CashAccount, account, new Decimal(100), CurrencyEnum.CanadianDollar);

            // act
            this._service.Transfer(customer, customer.CashAccount, account, new decimal(300), CurrencyEnum.USDollar);

            // assert
            Assert.AreEqual(700, this._service.GetBalance(account));
        }

        [Test]
        public void ShouldCreateWithdrawal()
        {
            // case 2: initial balance: 35000.00 CAD
            // arrange
            Account account = new Account(Guid.NewGuid());
            Customer customer = this.CreateCustomer("Glenn", "Quagmire", new List<Account>(){account});
            this._service.AddAccount(account);
            this._service.AddAccount(customer.CashAccount);
            this._service.Transfer(customer, customer.CashAccount, account, new Decimal(35000), CurrencyEnum.CanadianDollar);

            // act
            this._service.Transfer(customer, account, customer.CashAccount, new Decimal(5000), CurrencyEnum.MexicanPeso);
            this._service.Transfer(customer, account, customer.CashAccount, new Decimal(12500), CurrencyEnum.USDollar);
            this._service.Transfer(customer, customer.CashAccount, account, new Decimal(300), CurrencyEnum.CanadianDollar);

            // assert
            Assert.AreEqual(9800, this._service.GetBalance(account));
        }

        [Test]
        public void ShouldCreateTransferAmongMexicanCanadianAccountsForACustomer()
        {
            // case 3
            // account 1010: 7425
            // account 5500: 15000
            // arrange
            Account account1010 = new Account(Guid.NewGuid());
            Account account5500 = new Account(Guid.NewGuid());
            Customer customer = this.CreateCustomer("Joe", "Swanson", new List<Account>(){account1010, account5500});
            this._service.AddAccount(account1010);
            this._service.AddAccount(account5500);
            this._service.AddAccount(customer.CashAccount);
            this._service.Transfer(customer, customer.CashAccount, account1010, new Decimal(7425), CurrencyEnum.CanadianDollar);
            this._service.Transfer(customer, customer.CashAccount, account5500, new Decimal(15000), CurrencyEnum.CanadianDollar);

            // act
            this._service.Transfer(customer, account5500, customer.CashAccount, new Decimal(5000), CurrencyEnum.CanadianDollar);
            this._service.Transfer(customer, account1010, account5500, new Decimal(7300), CurrencyEnum.CanadianDollar);
            this._service.Transfer(customer, customer.CashAccount, account1010, new Decimal(13726), CurrencyEnum.MexicanPeso);

            // assert
            Assert.AreEqual(1497.60, this._service.GetBalance(account1010));
            Assert.AreEqual(17300, this._service.GetBalance(account5500));
        }

        [Test]
        public void ShouldCreateTransferAmongCustomers()
        {
            // case 4
            // account 0123: 150
            // account 456: 65000
            // arrange
            Account account0123 = new Account(Guid.NewGuid());
            Account account0456 = new Account(Guid.NewGuid());
            Customer customer = this.CreateCustomer("Peter", "Griffin", new List<Account>(){account0123});
            Customer customer1 = this.CreateCustomer("Lois", "Griffin", new List<Account>(){account0456});
            this._service.AddAccount(account0123);
            this._service.AddAccount(account0456);
            this._service.AddAccount(customer.CashAccount);
            this._service.AddAccount(customer1.CashAccount);
            this._service.Transfer(customer, customer.CashAccount, account0123, new Decimal(150), CurrencyEnum.CanadianDollar);
            this._service.Transfer(customer1, customer1.CashAccount, account0456, new Decimal(65000), CurrencyEnum.CanadianDollar);

            // act
            this._service.Transfer(customer, account0123, customer.CashAccount, new Decimal(70), CurrencyEnum.USDollar);
            this._service.Transfer(customer1, customer1.CashAccount, account0456, new Decimal(23789), CurrencyEnum.USDollar);
            this._service.Transfer(customer1, account0456, account0123, new Decimal(23.75), CurrencyEnum.CanadianDollar);

            // assert
            Assert.AreEqual(33.75, this._service.GetBalance(account0123));
            Assert.AreEqual(112554.25, this._service.GetBalance(account0456));
        }

        [Test]
        public void ShouldNotTransferMoney()
        {
            // case 5
            // account 1010: 7425
            // arrange
            Account account1010 = new Account(Guid.NewGuid());
            Customer customer = this.CreateCustomer("Joe", "Swanson", new List<Account>(){account1010});
            this._service.AddAccount(account1010);
            this._service.AddAccount(customer.CashAccount);
            this._service.Transfer(customer, customer.CashAccount, account1010, new Decimal(7425), CurrencyEnum.CanadianDollar);

            // act
            Customer thief = this.CreateCustomer("John", "Shark", new List<Account>());
            this._service.AddAccount(thief.CashAccount);
            this._service.Transfer(thief, account1010, thief.CashAccount, new Decimal(100), CurrencyEnum.USDollar);

            // assert
            Assert.AreEqual(7425, this._service.GetBalance(account1010));
            Assert.AreEqual(0, this._service.GetBalance(thief.CashAccount));
        }

        private Customer CreateCustomer(string firstName, string lastName, List<Account> accounts) {
            Account cashAccount = new Account(Guid.NewGuid());
            return new Customer(Guid.NewGuid(), firstName, lastName, accounts, cashAccount);
        }
    }
}