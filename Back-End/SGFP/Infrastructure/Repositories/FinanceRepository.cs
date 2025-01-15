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

        public async Task<IEnumerable<Finance>> GetAllExpensesAsync(Guid userId)
        {
            var query = "SELECT * FROM finances WHERE categ = -1 AND user_id = @Id";
            return await _dbConnection.QueryAsync<Finance>(query, new {Id = userId});
        }

        public async Task<IEnumerable<Finance>> GetAllRevenueAsync(Guid userId)
        {
            var query = "SELECT * FROM finances WHERE categ = 1 AND user_id = @Id";
            return await _dbConnection.QueryAsync<Finance>(query, new {Id = userId});
        }

        public async Task<IEnumerable<Finance>> GetByUserAsync(Guid userId)
        {
            var query = "SELECT * FROM finances WHERE User_Id = @UserId";
            return await _dbConnection.QueryAsync<Finance>(query, new {UserId = userId});
        }

        public async Task AddAsync(Finance finance)
        {
            var query = @"
                INSERT INTO finances (Id, Description, Categ, User_Id, Sub_Categ, Register_Date, Value)
                VALUES (@Id, @Description, @Categ, @UserId, @SubCateg, @RegisterDate, @Value)";
            await _dbConnection.ExecuteAsync(query, new { finance.Id, finance.Description, finance.Categ, finance.UserId, finance.SubCateg, finance.RegisterDate, finance.Value});
        }

        public async Task UpdateAsync(Finance finance)
        {
            var query = @"
                UPDATE finances
                SET Description = @Description, Categ = @Categ, User_Id = @UserId, Sub_Categ = @SubCateg, Register_Date = @RegisterDate, Value = @Value
                WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new { finance.Id, finance.Description, finance.Categ, finance.UserId, finance.SubCateg, finance.RegisterDate, finance.Value});
        }

        public async Task DeleteAsync(Guid id)
        {
            var query = "DELETE FROM finances WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id });
        }
    }
}
