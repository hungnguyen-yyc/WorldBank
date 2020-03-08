using WorldBank.Models.Transaction;
using System;

namespace WorldBank.Models.User {
    public class Account : Entity {

        public Account(Guid id) : base(id) {}

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Account)) {
                Account otherAccount = (Account)obj;
                return otherAccount.Id == this.Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}