using SGFP.Application.DTOs;
using SGFP.Domain.Entities;
using SGFP.Domain.Interfaces;
using SGFP.Infrastructure.Repositories;

namespace SGFP.Domain.Services
{
    public class FinanceService
    {
        private readonly IFinanceRepository _repository;

        private readonly IUserRepository _userRepository;

        public FinanceService(IFinanceRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<Finance> GetByIdAsync(Guid id)
        {
            var financeGet = await _repository.GetByIdAsync(id);
            if (financeGet == null)
            {
                throw new HttpRequestException("Movimentação não encontrada!");
            }
            Finance finance = financeGet;
            return finance;
        }

        public async Task<IEnumerable<Finance>> GetAllAsync()
        {
            List<Finance> finances = new();
            foreach (var finance in await _repository.GetAllAsync())
            {
                finances.Add(finance);
            }
            return finances;

        }

        public async Task<IEnumerable<Finance>> GetAllExpensesAsync(Guid userId)
        {

            var finances = await _repository.GetAllExpensesAsync(userId);
            List<Finance> financesList = finances.ToList();
            return finances;

        }

        public async Task<IEnumerable<Finance>> GetAllRevenueAsync(Guid userId)
        {
            var finances = await _repository.GetAllRevenueAsync(userId);
            List<Finance> financesList = finances.ToList();
            return finances;

        }

        public async Task<IEnumerable<Finance>> GetByUserAsync(Guid userid)
        {
            if (await _userRepository.GetByIdAsync(userid) == null)
            {
                throw new BadHttpRequestException("Logue primeiro");
            }
            List<Finance> finances = new();
            foreach (var finance in await _repository.GetByUserAsync(userid))
            {
                finances.Add(finance);
            }
            return finances;
        }

        public async Task AddAsync(FinanceDTO financeDTO)
        {
            if (await _userRepository.GetByIdAsync(financeDTO.UserId) == null)
            {
                throw new BadHttpRequestException("Logue primeiro");
            }
            Finance finance = new Finance(financeDTO.Description, financeDTO.Categ, financeDTO.UserId, financeDTO.SubCateg, financeDTO.Value);
            await _repository.AddAsync(finance);
        }

        public async Task UpdateAsync(Guid id, FinanceDTO financeDTO)
        {
            if (await _repository.GetByIdAsync(id) == null)
            {
                throw new HttpRequestException("Movimentação não encontrada!");
            }
            if (await _userRepository.GetByIdAsync(financeDTO.UserId) == null)
            {
                throw new BadHttpRequestException("Logue primeiro");
            }
            Finance finance = new Finance(id, financeDTO.Description, financeDTO.Categ, financeDTO.UserId, financeDTO.SubCateg, financeDTO.Value);
            await _repository.UpdateAsync(finance);
        }

        public async Task DeleteAsync(Guid id)
        {
            if (await _repository.GetByIdAsync(id) == null)
            {
                throw new HttpRequestException("Movimentação não encontrada!");
            }
            await _repository.DeleteAsync(id);
        }
    }
}
