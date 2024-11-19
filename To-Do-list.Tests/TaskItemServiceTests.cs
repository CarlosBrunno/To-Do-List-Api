using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using To_Do_List.Application.Services;
using To_Do_List.Domain.Entities;
using To_Do_List.Domain.Interfaces;
using To_Do_List.Tests.InMemoryRepositories;
using Xunit;

namespace To_Do_List.Tests.Services
{
    public class TaskItemServiceTests
    {
        private readonly ITaskItemRepository _taskItemRepositoryMock;
        private readonly TaskItemService _taskItemService;

        public TaskItemServiceTests()
        {
            _taskItemRepositoryMock = new InMemoryTaskItemRepository();
            _taskItemService = new TaskItemService(_taskItemRepositoryMock);
        }

        [Fact]
        public async Task AddTaskItem()
        {
            // Arrange
            var newTask = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = "New Task",
                Description = "Description of the new task",
                IsChecked = false,
                CreatedAt = DateTime.UtcNow
            };

            // Act
            await _taskItemService.AddAsync(newTask);
            var allTasks = await _taskItemService.GetAllAsync();

            // Assert
            Assert.Contains(allTasks, t => t.Id == newTask.Id);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnTaskItems()
        {
            var taskItems = new List<TaskItem>
            {
                new TaskItem { Id = Guid.NewGuid(), Title = "Task 1", IsChecked = false },
                new TaskItem { Id = Guid.NewGuid(), Title = "Task 2", IsChecked = true }
            };

            _taskItemRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(taskItems);

            var result = await _taskItemService.GetAllAsync();

            result.Should().BeEquivalentTo(taskItems);
            _taskItemRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnTaskItem_WhenTaskItemExists()
        {
            var taskId = Guid.NewGuid();
            var taskItem = new TaskItem { Id = taskId, Title = "Task 1", IsChecked = false };

            _taskItemRepositoryMock
                .Setup(repo => repo.GetByIdAsync(taskId))
                .ReturnsAsync(taskItem);

            var result = await _taskItemService.GetByIdAsync(taskId);

            result.Should().BeEquivalentTo(taskItem);
            _taskItemRepositoryMock.Verify(repo => repo.GetByIdAsync(taskId), Times.Once);
        }

        

        [Fact]
        public async Task UpdateAsync_ShouldCallRepositoryUpdateMethod()
        {
            var updatedTaskItem = new TaskItem { Id = Guid.NewGuid(), Title = "Updated Task" };

            await _taskItemService.UpdateAsync(updatedTaskItem);

            _taskItemRepositoryMock.Verify(repo => repo.UpdateAsync(updatedTaskItem), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryDeleteMethod()
        {
            var taskId = Guid.NewGuid();

            await _taskItemService.DeleteAsync(taskId);

            _taskItemRepositoryMock.Verify(repo => repo.DeleteAsync(taskId), Times.Once);
        }

       /* [Fact]
        public async Task AddAsync_ShouldThrowException_WhenTitleIsNull()
        {
            // Arrange
            var newTaskItem = new TaskItem { Id = Guid.NewGuid() };

            // Act
            await _taskItemService.AddAsync(newTaskItem);
            var result = await _taskItemService.GetByIdAsync(newTaskItem.Id);

            // Assert
            _taskItemRepositoryMock.Verify(repo => repo.AddAsync(newTaskItem), Times.Once);
        }*/

    }
}
