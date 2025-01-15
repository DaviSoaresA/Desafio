using SGFP.Domain.Entities;

namespace SGFP.Application.DTOs
{
    public class FinanceDTO
    {
        public FinanceDTO(string description, MovementType categ, Guid userId, string subCateg, double value)
        {
            this.Description = description;
            this.Categ = categ;
            this.UserId = userId;
            this.SubCateg = subCateg;
            this.Value = value;
        }

        public string Description { get; private set; }
        public MovementType Categ { get; private set; }
        public Guid UserId { get; private set; }
        public string SubCateg { get; private set; }
        public double Value { get; private set; }
    }
}
