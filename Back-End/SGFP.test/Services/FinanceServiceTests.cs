using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using SGFP.Application.DTOs;
using SGFP.Domain.Entities;
using SGFP.Domain.Interfaces;
using SGFP.Domain.Services;
using Xunit;

namespace SGFP.Tests.Services
{
    public class FinanceServiceTests
    {
        private readonly Mock<IFinanceRepository> _financeRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly FinanceService _financeService;

        public FinanceServiceTests()
        {
            _financeRepositoryMock = new Mock<IFinanceRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _financeService = new FinanceService(_financeRepositoryMock.Object, _userRepositoryMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnFinance_WhenFinanceExists()
        {
            // Arrange
            var financeId = Guid.NewGuid();
            var finance = new Finance("Description", (MovementType)(-1), Guid.NewGuid(), "SubCateg", 100);
            _financeRepositoryMock.Setup(r => r.GetByIdAsync(financeId)).ReturnsAsync(finance);

            // Act
            var result = await _financeService.GetByIdAsync(financeId);

            // Assert
            Assert.Equal(finance, result);
            _financeRepositoryMock.Verify(r => r.GetByIdAsync(financeId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowException_WhenFinanceDoesNotExist()
        {
            // Arrange
            var financeId = Guid.NewGuid();
            _financeRepositoryMock.Setup(r => r.GetByIdAsync(financeId)).ReturnsAsync((Finance)null);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => _financeService.GetByIdAsync(financeId));
        }

        [Fact]
        public async Task AddAsync_ShouldCallRepositoryAdd_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var financeDTO = new FinanceDTO
            (
                "Description",
                (MovementType)(-1),
                userId,
                "SubCateg",
                100
            );
            _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(new User());

            // Act
            await _financeService.AddAsync(financeDTO);

            // Assert
            _financeRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Finance>()), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var financeDTO = new FinanceDTO
            (
                "Description",
                (MovementType)(-1),
                userId,
                "SubCateg",
                100
            );
            _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync((User)null);

            // Act & Assert
            await Assert.ThrowsAsync<BadHttpRequestException>(() => _financeService.AddAsync(financeDTO));
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryDelete_WhenFinanceExists()
        {
            // Arrange
            var financeId = Guid.NewGuid();
            var finance = new Finance("Description", (MovementType)(-1), Guid.NewGuid(), "SubCateg", 100);
            _financeRepositoryMock.Setup(r => r.GetByIdAsync(financeId)).ReturnsAsync(finance);

            // Act
            await _financeService.DeleteAsync(financeId);

            // Assert
            _financeRepositoryMock.Verify(r => r.DeleteAsync(financeId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenFinanceDoesNotExist()
        {
            // Arrange
            var financeId = Guid.NewGuid();
            _financeRepositoryMock.Setup(r => r.GetByIdAsync(financeId)).ReturnsAsync((Finance)null);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => _financeService.DeleteAsync(financeId));
        }
    }
}
