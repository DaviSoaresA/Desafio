using SGFP.Domain.Entities;

namespace SGFP.Domain.Interfaces
{
    public interface IFinanceRepository
    {
        Task<Finance> GetByIdAsync(Guid id);
        Task<IEnumerable<Finance>> GetAllAsync();
        Task<IEnumerable<Finance>> GetByUserAsync(Guid userId);
        Task AddAsync(Finance finance);
        Task UpdateAsync(Finance finance);
        Task DeleteAsync(Guid id);
    }
}
