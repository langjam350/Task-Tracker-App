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
        public void AddTask_ReturnsTrue_WhenRepositoryReturnsTrue()
        {
            // Arrange
            var title = "Test Task";
            _mockRepo.Setup(r => r.AddTaskToRepository(title)).Returns(true);

            // Act
            var result = _service.AddTask(title);

            // Assert
            Assert.IsTrue(result);
            _mockRepo.Verify(r => r.AddTaskToRepository(title), Times.Once);
        }

        [Test]
        public void AddTask_ReturnsFalse_WhenRepositoryReturnsFalse()
        {
            // Arrange
            var title = "Test Task";
            _mockRepo.Setup(r => r.AddTaskToRepository(title)).Returns(false);

            // Act
            var result = _service.AddTask(title);

            // Assert
            Assert.IsFalse(result);
            _mockRepo.Verify(r => r.AddTaskToRepository(title), Times.Once);
        }

        [Test]
        public void GetAllTasks_ReturnsTasksFromRepository()
        {
            // Arrange
            var expectedTasks = new List<TaskItem>
            {
                new TaskItem("Task 1") { Id = 1, IsCompleted = false },
                new TaskItem("Task 2") { Id = 2, IsCompleted = true }
            };
            _mockRepo.Setup(r => r.GetAllItems()).Returns(expectedTasks);

            // Act
            var result = _service.GetAllTasks();

            // Assert
            Assert.AreEqual(expectedTasks, result);
            _mockRepo.Verify(r => r.GetAllItems(), Times.Once);
        }

        [Test]
        public void CompleteTask_ReturnsTrue_WhenRepositoryReturnsTrue()
        {
            // Arrange
            int taskId = 1;
            _mockRepo.Setup(r => r.CompleteTask(taskId)).Returns(true);

            // Act
            var result = _service.CompleteTask(taskId);

            // Assert
            Assert.IsTrue(result);
            _mockRepo.Verify(r => r.CompleteTask(taskId), Times.Once);
        }

        [Test]
        public void CompleteTask_ReturnsFalse_WhenRepositoryReturnsFalse()
        {
            // Arrange
            int taskId = 1;
            _mockRepo.Setup(r => r.CompleteTask(taskId)).Returns(false);

            // Act
            var result = _service.CompleteTask(taskId);

            // Assert
            Assert.IsFalse(result);
            _mockRepo.Verify(r => r.CompleteTask(taskId), Times.Once);
        }
    }
}