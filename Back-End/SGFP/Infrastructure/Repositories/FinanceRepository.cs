using System.Data;
using Dapper;
using SGFP.Domain.Entities;
using SGFP.Domain.Interfaces;

namespace SGFP.Infrastructure.Repositories
{
    public class FinanceRepository : IFinanceRepository
    {
        private readonly IDbConnection _dbConnection;

        public FinanceRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Finance> GetByIdAsync(Guid id)
        {
            var query = "SELECT * FROM finances WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<Finance>(query, new { Id = id });
        }

        public async Task<IEnumerable<Finance>> GetAllAsync()
        {
            var query = "SELECT * FROM finances";
            return await _dbConnection.QueryAsync<Finance>(query);
        }

        public async Task<IEnumerable<Finance>> GetByUserAsync(Guid userId)
        {
            var query = "SELECT * FROM finances WHERE UserId = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<IEnumerable<Finance>>(query, new {Id = userId});
        }

        public async Task AddAsync(Finance finance)
        {
            var query = @"
                INSERT INTO finances (Id, Description, Categ, User, SubCateg)
                VALUES (@Id, @Description, @Categ, @User, @SubCateg)";
            await _dbConnection.ExecuteAsync(query, new { finance.Id, finance.Description, finance.Categ, finance.User, finance.SubCateg});
        }

        public async Task UpdateAsync(Finance finance)
        {
            var query = @"
                UPDATE finances
                SET Description = @Description, Categ = @Categ, User = @User, SubCateg = @SubCateg
                WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new { finance.Id, finance.Description, finance.Categ, finance.User, finance.SubCateg });
        }

        public async Task DeleteAsync(Guid id)
        {
            var query = "DELETE FROM finances WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id });
        }
    }
}
