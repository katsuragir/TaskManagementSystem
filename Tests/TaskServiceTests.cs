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
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _repositoryMock;
        private readonly Mock<ILoggerService> _loggerMock;
        private readonly TaskService _taskService;

        public TaskServiceTests()
        {
            _repositoryMock = new Mock<ITaskRepository>();
            _loggerMock = new Mock<ILoggerService>();
            _taskService = new TaskService(_repositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnTaskResponse()
        {
            var request = new CreateTaskRequest
            {
                Title = "Test",
                Description = "Description",
                DueDate = DateTime.Now.AddDays(1),
                Priority = "High",
                Status = "Pending"
            };

            var result = await _taskService.CreateAsync(request);

            Assert.Equal("Test", result.Title);
            _repositoryMock.Verify(x => x.AddAsync(It.IsAny<TaskItem>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllTasks()
        {
            _repositoryMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<TaskItem> {
                    new TaskItem { Title = "Task1" },
                    new TaskItem { Title = "Task2" }
                });

            var result = await _taskService.GetAllAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task UpdateAsync_WithValidId_ShouldReturnTrue()
        {
            var id = Guid.NewGuid();
            _repositoryMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(new TaskItem { Id = id, Title = "Old Title" });

            var request = new UpdateTaskRequest { Title = "New Title" };
            var result = await _taskService.UpdateAsync(id, request);

            Assert.True(result);
            _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<TaskItem>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WithInvalidId_ShouldReturnFalse()
        {
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((TaskItem?)null);

            var result = await _taskService.DeleteAsync(Guid.NewGuid());

            Assert.False(result);
            _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Never);
        }
    }
}
