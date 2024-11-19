using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using To_Do_List.Domain.Entities;
using To_Do_List.Domain.Interfaces;

namespace To_Do_List.Tests.InMemoryRepositories
{
    public class InMemoryTaskItemRepository : ITaskItemRepository
    {
        private readonly List<TaskItem> _taskItems = new();

        public Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return Task.FromResult(_taskItems.AsEnumerable());
        }

        public Task<TaskItem> GetByIdAsync(Guid id)
        {
            var taskItem = _taskItems.FirstOrDefault(t => t.Id == id);
            return Task.FromResult(taskItem);
        }

        public Task AddAsync(TaskItem taskItem)
        {
            _taskItems.Add(taskItem);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(TaskItem taskItem)
        {
            var existingTask = _taskItems.FirstOrDefault(t => t.Id == taskItem.Id);
            if (existingTask == null)
            {
                throw new ArgumentException("Task not found for task id: " + taskItem.Id);
            }

            existingTask.Title = taskItem.Title;
            existingTask.Description = taskItem.Description;
            existingTask.IsChecked = taskItem.IsChecked;
            existingTask.UpdatedAt = DateTime.UtcNow;

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            var taskItem = _taskItems.FirstOrDefault(t => t.Id == id);
            if (taskItem == null)
            {
                throw new ArgumentException("Task not found for task id: " + id);
            }

            _taskItems.Remove(taskItem);
            return Task.CompletedTask;
        }
    }
}
