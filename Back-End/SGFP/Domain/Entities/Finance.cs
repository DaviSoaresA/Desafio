using System.ComponentModel.DataAnnotations.Schema;

namespace SGFP.Domain.Entities
{
    [Table("finances")]
    public class Finance
    {
        public Finance()
        {
            
        }
        public Finance(string description, MovementType categ, Guid userId, string subCateg)
        {
            this.Description = description;
            this.Categ = categ;
            this.UserId = userId;
            this.SubCateg = subCateg;
            Id = Guid.NewGuid();
            RegisterDate = DateTime.UtcNow;
        }
        public Guid Id { get; init; }

        public string Description { get; private set; }

        public MovementType Categ { get; private set; }
        public Guid UserId { get; private set; }
        public User? User { get; private set; }

        public string SubCateg { get; private set; }

        public DateTime RegisterDate { get; init; } 

    }

    public enum MovementType
    {
        EXPENSE = -1, REVENUE = 1
    }
}
