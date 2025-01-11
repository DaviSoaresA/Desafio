using System.Data;
using Dapper;
using SGFP.Domain.Entities;
using SGFP.Domain.Interfaces;
using SGFP.Infrastructure.Data;

namespace SGFP.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var query = "SELECT * FROM Users WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<User>(query, new {Id = id});
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var query = "SELECT * FROM Users";
            return await _dbConnection.QueryAsync<User>(query);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var query = "SELECT * FROM Users WHERE Email = @Email";
            return await _dbConnection.QueryFirstOrDefaultAsync<User>(query, new { Email = email });
        }

        public async Task AddAsync(User user)
        {
            var query = @"
                INSERT INTO Users (Id, Name, Email, Password, Finances)
                VALUES (@Id, @Name, @Email, @Password, @Finances)";
            await _dbConnection.ExecuteAsync(query, new {user.Id, user.Name, user.Email, user.Password, user.Finances});
        }

        public async Task UpdateAsync(User user)
        {
            var query = @"
                UPDATE Users
                SET Name = @Name, Email = @Email, Password = @Password, Finances = @Finances
                WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new {user.Id, user.Name, user.Email, user.Password, user.Finances});
        }

        public async Task DeleteAsync(Guid id)
        {
            var query = "DELETE FROM Users WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id });
        }
    }
}
