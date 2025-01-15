using System.ComponentModel.DataAnnotations.Schema;

namespace SGFP.Domain.Entities
{
    [Table("finances")]
    public class Finance
    {
        public Finance()
        {
            
        }
        public Finance(Guid id, string description, MovementType categ, Guid userId, string subCateg, double value)
        {
            this.Id = id;
            this.Description = description;
            this.Categ = categ;
            this.UserId = userId;
            this.SubCateg = subCateg;
            this.Value = value;
            RegisterDate = DateTime.UtcNow;
        }
        public Finance(string description, MovementType categ, Guid userId, string subCateg, double value)
        {
            this.Description = description;
            this.Categ = categ;
            this.UserId = userId;
            this.SubCateg = subCateg;
            this.Value = value;
            Id = Guid.NewGuid();
            RegisterDate = DateTime.UtcNow;
        }
        [Column("id")]
        public Guid Id { get; init; }
        [Column("description")]
        public string Description { get; private set; }
        [Column("categ")]
        public MovementType Categ { get; private set; }
        [Column("user_id")]
        public Guid UserId { get; private set; }
        public User? User { get; private set; }
        [Column("sub_categ")]
        public string SubCateg { get; private set; }
        [Column("register_date")]
        public DateTime RegisterDate { get; init; }
        [Column("value")]
        public double Value { get; private set; }

    }

    public enum MovementType
    {
        EXPENSE = -1, REVENUE = 1
    }
}
