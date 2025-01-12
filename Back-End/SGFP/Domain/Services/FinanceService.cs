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
            if (_repository.GetByIdAsync(id) == null)
            {
                throw new HttpRequestException("Movimentação não encontrada!");
            }
            Finance finance = await _repository.GetByIdAsync(id);
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

        public async Task AddAsync(FinanceDTO financeDTO)
        {
            if (_userRepository.GetByIdAsync(financeDTO.UserId) == null)
            {
                throw new BadHttpRequestException("Logue primeiro");
            }
            Finance finance = new Finance(financeDTO.Description, financeDTO.Categ, financeDTO.UserId, financeDTO.SubCateg);
            await _repository.AddAsync(finance);
        }

        public async Task UpdateAsync(Guid id, FinanceDTO financeDTO)
        {
            if (_repository.GetByIdAsync(id) == null)
            {
                throw new HttpRequestException("Movimentação não encontrada!");
            }
            if (_userRepository.GetByIdAsync(financeDTO.UserId) == null)
            {
                throw new BadHttpRequestException("Logue primeiro");
            }
            Finance finance = await _repository.GetByIdAsync(id);
            await _repository.UpdateAsync(finance);
        }

        public async Task DeleteAsync(Guid id)
        {
            if (_repository.GetByIdAsync(id) == null)
            {
                throw new HttpRequestException("Movimentação não encontrada!");
            }
            await _repository.DeleteAsync(id);
        }
    }
}
