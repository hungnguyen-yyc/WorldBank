using System;

namespace WorldBank.Models{
    public abstract class Entity : IEntity {
        
        private Guid _id;

        public Entity(Guid id) {
            this._id = id;
        }

        public Guid Id { get => this._id; set => this._id = value;}
    }
}