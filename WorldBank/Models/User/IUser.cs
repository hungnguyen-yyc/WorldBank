namespace WorldBank.Models.User {
    public interface IUser : IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; }
    }
}