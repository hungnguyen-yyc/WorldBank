using System;

namespace WorldBank.Models.User {
    /*
    could have separ
    */
    public abstract class User : IUser {

        private Guid _id;
        private string _firstName;
        private string _lastName;

        public User(Guid id, string firstName, string lastName) {
            this._id = id;
            this._firstName = firstName;
            this._lastName = lastName;
        }

        public Guid Id { get => this._id; set => this._id = value; }
        public string FirstName { get => this._firstName; set => this._firstName = value; }
        public string LastName { get => this._lastName; set => this._lastName = value; }
        public string FullName { get => this._firstName + " " + this._lastName;}
    }
}