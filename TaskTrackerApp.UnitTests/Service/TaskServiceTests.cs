using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TaskTrackerApp.API.NETCore.DAL;
using TaskTrackerApp.API.NETCore.Model;
using TaskTrackerApp.API.NETCore.Service;

namespace TaskTrackerApp.UnitTests.Service
{
    [TestFixture]
    public class TaskServiceTests
    {
        private Mock<ITaskRepository> _mockRepo;
        private TaskService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<ITaskRepository>();
            _service = new TaskService(_mockRepo.Object);
        }

        [Test]
        public async Task AddTask_ReturnsTrue_WhenRepositoryReturnsTrue()
        {
            // Arrange
            var title = "Test Task";
            _mockRepo.Setup(r => r.AddTaskToRepositoryAsync(title)).ReturnsAsync(true);

            // Act
            var result = await _service.AddTaskAsync(title);

            // Assert
            Assert.IsTrue(result);
            _mockRepo.Verify(r => r.AddTaskToRepositoryAsync(title), Times.Once);
        }

        [Test]
        public async Task AddTask_ReturnsFalse_WhenRepositoryReturnsFalse()
        {
            // Arrange
            var title = "Test Task";
            _mockRepo.Setup(r => r.AddTaskToRepositoryAsync(title)).ReturnsAsync(false);

            // Act
            var result = await _service.AddTaskAsync(title);

            // Assert
            Assert.IsFalse(result);
            _mockRepo.Verify(r => r.AddTaskToRepositoryAsync(title), Times.Once);
        }

        [Test]
        public async Task GetAllTasks_ReturnsTasksFromRepository()
        {
            // Arrange
            var expectedTasks = new List<TaskItem>
            {
                new TaskItem("Task 1") { Id = 1, IsCompleted = false },
                new TaskItem("Task 2") { Id = 2, IsCompleted = true }
            };
            _mockRepo.Setup(r => r.GetAllItemsAsync()).ReturnsAsync(expectedTasks);

            // Act
            var result = await _service.GetAllTasksAsync();

            // Assert
            Assert.AreEqual(expectedTasks, result);
            _mockRepo.Verify(r => r.GetAllItemsAsync(), Times.Once);
        }

        [Test]
        public async Task CompleteTask_ReturnsTrue_WhenRepositoryReturnsTrue()
        {
            // Arrange
            int taskId = 1;
            _mockRepo.Setup(r => r.CompleteTaskAsync(taskId)).ReturnsAsync(true);

            // Act
            var result = await _service.CompleteTaskAsync(taskId);

            // Assert
            Assert.IsTrue(result);
            _mockRepo.Verify(r => r.CompleteTaskAsync(taskId), Times.Once);
        }

        [Test]
        public async Task CompleteTask_ReturnsFalse_WhenRepositoryReturnsFalse()
        {
            // Arrange
            int taskId = 1;
            _mockRepo.Setup(r => r.CompleteTaskAsync(taskId)).ReturnsAsync(false);

            // Act
            var result = await _service.CompleteTaskAsync(taskId);

            // Assert
            Assert.IsFalse(result);
            _mockRepo.Verify(r => r.CompleteTaskAsync(taskId), Times.Once);
        }
    }
}