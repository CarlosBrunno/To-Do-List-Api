using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using To_Do_List.Application.Services;
using To_Do_List.Domain.Entities;
using To_Do_List.Domain.Interfaces;
using To_Do_List.Infrastructure.Repositories;
using To_Do_List.Tests.Services;
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
                Id = new Guid(Guid.NewGuid().ToString()),
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
        public async Task GetByIdTaskItem()
        {
            // Arrange
            var newTask = new TaskItem
            {
                Id = new Guid(Guid.NewGuid().ToString()),
                Title = "New Task",
                Description = "Description of the new task",
                IsChecked = false,
                CreatedAt = DateTime.UtcNow
            };

            // Act
            await _taskItemService.AddAsync(newTask);
            var createdTask = await _taskItemService.GetByIdAsync(newTask.Id);

            // Assert
            Assert.Equal(newTask.Id, createdTask.Id);
        }

        [Fact]
        public async Task GetAllTaskItem()
        {
            // Arrange
            var taskItemsList = new List<TaskItem>
                {
                    new TaskItem {
                    Id = new Guid(Guid.NewGuid().ToString()),
                    Title = "New Task",
                    Description = "Description of the new task",
                    IsChecked = false,
                    CreatedAt = DateTime.UtcNow
                },
                    new TaskItem {
                    Id = new Guid(Guid.NewGuid().ToString()),
                    Title = "New Task2",
                    Description = "Description of the new task2",
                    IsChecked = false,
                    CreatedAt = DateTime.UtcNow
                }
            };

            // Act
            await _taskItemService.AddAsync(taskItemsList[0]);
            await _taskItemService.AddAsync(taskItemsList[1]);

            var taskItems = await _taskItemService.GetAllAsync();

            // Assert
            Assert.Equal(taskItems.Count(), 2);
        }


        [Fact]
        public async Task UpdateTaskItem()
        {
            // Arrange
            var newTask = new TaskItem
            {
                Id = new Guid(Guid.NewGuid().ToString()),
                Title = "New Task",
                Description = "Description of the new task",
                IsChecked = false,
                CreatedAt = DateTime.UtcNow
            };

            // Act
            await _taskItemService.AddAsync(newTask);
            newTask.Title = "Updated Task";
            await _taskItemService.UpdateAsync(newTask);
            var updatedTask = await _taskItemService.GetByIdAsync(newTask.Id);

            // Assert
            Assert.Equal(updatedTask.Title, "Updated Task");
        }

        [Fact]
        public async Task DeleteByIdTaskItem()
        {
            // Arrange
            var newTask = new TaskItem
            {
                Id = new Guid(Guid.NewGuid().ToString()),
                Title = "New Task",
                Description = "Description of the new task",
                IsChecked = false,
                CreatedAt = DateTime.UtcNow
            };

            // Act
            await _taskItemService.AddAsync(newTask);
            await _taskItemService.DeleteAsync(newTask.Id);
            var deletedTask = await _taskItemService.GetByIdAsync(newTask.Id);

            // Assert
            Assert.Equal(deletedTask, null);
        }

    }
}
