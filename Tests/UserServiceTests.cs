using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _repoMock;
        private readonly UserService _service;

        public UserServiceTests()
        {
            _repoMock = new Mock<IUserRepository>();
            _service = new UserService(_repoMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnUserResponse()
        {
            var request = new CreateUserRequest
            {
                Name = "Ridho",
                Email = "ridho@example.com"
            };

            var response = await _service.CreateAsync(request);

            Assert.Equal("Ridho", response.Name);
            Assert.Equal("ridho@example.com", response.Email);
            _repoMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllUsers()
        {
            _repoMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<User> {
                    new User { Name = "A" },
                    new User { Name = "B" }
                });

            var result = await _service.GetAllAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnTrue_WhenUserExists()
        {
            var id = Guid.NewGuid();
            _repoMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(new User { Id = id, Name = "Old" });

            var result = await _service.UpdateAsync(id, new UpdateUserRequest { Name = "New" });

            Assert.True(result);
            _repoMock.Verify(r => r.UpdateAsync(It.Is<User>(u => u.Name == "New")), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenUserNotFound()
        {
            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((User?)null);

            var result = await _service.DeleteAsync(Guid.NewGuid());

            Assert.False(result);
            _repoMock.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Never);
        }
    }
}