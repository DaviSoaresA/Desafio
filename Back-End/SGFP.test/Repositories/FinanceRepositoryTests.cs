using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Moq;
using SGFP.Domain.Entities;
using SGFP.Infrastructure.Repositories;
using Xunit;

namespace SGFP.Tests.Repositories
{
    public class FinanceRepositoryTests
    {
        private readonly Mock<IDbConnection> _dbConnectionMock;
        private readonly FinanceRepository _financeRepository;

        public FinanceRepositoryTests()
        {
            _dbConnectionMock = new Mock<IDbConnection>();
            _financeRepository = new FinanceRepository(_dbConnectionMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnFinance_WhenFinanceExists()
        {
            // Arrange
            var financeId = Guid.NewGuid();
            var expectedFinance = new Finance
            (
                financeId,
                "Test Finance",
                (MovementType)1,
                Guid.NewGuid(),
                "Test Subcategory",
                100.0
            );

            _dbConnectionMock
                .Setup(c => c.QueryFirstOrDefaultAsync<Finance>(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null))
                .ReturnsAsync(expectedFinance);

            // Act
            var result = await _financeRepository.GetByIdAsync(financeId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedFinance.Id, result.Id);
            Assert.Equal(expectedFinance.Description, result.Description);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfFinances()
        {
            // Arrange
            var expectedFinances = new List<Finance>
            {
                new Finance
                (
                    Guid.NewGuid(),
                    "Finance 1",
                    (MovementType)(1),
                    Guid.NewGuid(),
                    "Category 1",
                    150.0
                ),
                new Finance
                (
                    Guid.NewGuid(),
                    "Finance 2",
                    (MovementType)(-1),
                    Guid.NewGuid(),
                    "Category 2",
                    200.0
                )
            };

            _dbConnectionMock
                .Setup(c => c.QueryAsync<Finance>(
                    It.IsAny<string>(),
                    null, null, null, null))
                .ReturnsAsync(expectedFinances);

            // Act
            var result = await _financeRepository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, expectedFinances.Count);
        }

        [Fact]
        public async Task AddAsync_ShouldExecuteInsertQuery()
        {
            // Arrange
            var finance = new Finance
            (
                Guid.NewGuid(),
                "New Finance",
                (MovementType)1,
                Guid.NewGuid(),
                "New Subcategory",
                250.0
            );

            _dbConnectionMock
                .Setup(c => c.ExecuteAsync(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null))
                .ReturnsAsync(1); // Simula 1 linha afetada

            // Act
            await _financeRepository.AddAsync(finance);

            // Assert
            _dbConnectionMock.Verify(c =>
                c.ExecuteAsync(
                    It.IsAny<string>(),
                    It.Is<object>(p =>
                        (Guid)p.GetType().GetProperty("Id").GetValue(p) == finance.Id &&
                        (string)p.GetType().GetProperty("Description").GetValue(p) == finance.Description),
                    null, null, null),
                Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldExecuteDeleteQuery()
        {
            // Arrange
            var financeId = Guid.NewGuid();

            _dbConnectionMock
                .Setup(c => c.ExecuteAsync(
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null))
                .ReturnsAsync(1); // Simula 1 linha afetada

            // Act
            await _financeRepository.DeleteAsync(financeId);

            // Assert
            _dbConnectionMock.Verify(c =>
                c.ExecuteAsync(
                    It.IsAny<string>(),
                    It.Is<object>(p => (Guid)p.GetType().GetProperty("Id").GetValue(p) == financeId),
                    null, null, null),
                Times.Once);
        }
    }
}
