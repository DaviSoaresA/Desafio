namespace SGFP.Domain.Entities
{
    public class User
    {
        public User(Guid id, string name, string email, string password, ICollection<Finance> finances)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Finances = finances;
        }
        public User(string name, string email, string password)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Finances = new List<Finance>();
            Id = Guid.NewGuid();
        }
        public Guid Id { get; init; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public ICollection<Finance> Finances { get; private set; }
    }
}
