using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using SGFP.Application.DTOs;
using SGFP.Domain.Entities;
using SGFP.Domain.Interfaces;
using SGFP.Domain.Services;
using Xunit;

namespace SGFP.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
            _userRepositoryMock.Reset();
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnUserDTO_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User("Test User", "test@example.com", "password123");
            _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);

            // Act
            var result = await _userService.GetByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Name, result.Name);
            Assert.Equal(user.Email, result.Email);
            _userRepositoryMock.Verify(r => r.GetByIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync((User)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _userService.GetByIdAsync(userId));
        }

        [Fact]
        public async Task AddAsync_ShouldAddUser_WhenEmailIsUnique()
        {
            // Arrange
            var userInsertDTO = new UserInsertDTO
            (
                "New User",
                "new@example.com",
                "password123",
                "password123"
            );
            _userRepositoryMock.Setup(r => r.GetByEmailAsync(userInsertDTO.Email)).ReturnsAsync((User)null);

            // Act
            await _userService.AddAsync(userInsertDTO);

            // Assert
            _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowException_WhenPasswordsDoNotMatch()
        {
            // Arrange
            var userInsertDTO = new UserInsertDTO
            (
                "New User",
                "new@example.com",
                "password123",
                "password124"
            );

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _userService.AddAsync(userInsertDTO));
        }

        [Fact]
        public async Task AddAsync_ShouldThrowException_WhenEmailAlreadyExists()
        {
            // Arrange
            var userInsertDTO = new UserInsertDTO
            (
                "New User",
                "new@example.com",
                "password123",
                "password123"
            );
            var existingUser = new User("Existing User", "existing@example.com", "password123");
            _userRepositoryMock.Setup(r => r.GetByEmailAsync(userInsertDTO.Email)).ReturnsAsync(existingUser);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _userService.AddAsync(userInsertDTO));
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryDelete_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User("Existing User", "existing@example.com", "password123");
            _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);

            // Act
            await _userService.DeleteAsync(userId);

            // Assert
            _userRepositoryMock.Verify(r => r.DeleteAsync(userId), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync((User)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _userService.DeleteAsync(userId));
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateUser_WhenValidData()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var existingUser = new User(userId, "Existing User", "old@example.com", "password123", null);
            var userDTO = new UserDTO("Updated User", "updated@example.com");
            _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(existingUser);
            _userRepositoryMock.Setup(r => r.GetByEmailAsync(userDTO.Email)).ReturnsAsync((User)null);

            // Act
            await _userService.UpdateAsync(userId, userDTO);

            // Assert
            _userRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userDTO = new UserDTO("Updated User", "updated@example.com");
            _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync((User)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _userService.UpdateAsync(userId, userDTO));
        }
    }
}
