using System.ComponentModel.DataAnnotations.Schema;

namespace SGFP.Domain.Entities
{
    [Table("users")]
    public class User
    {
        public User()
        {
            
        }
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
        [Column("id")]
        public Guid Id { get; init; }
        [Column("name")]
        public string Name { get; private set; }
        [Column("email")]
        public string Email { get; private set; }
        [Column("password")]
        public string Password { get; private set; }
        public ICollection<Finance> Finances { get; private set; }
    }
}
