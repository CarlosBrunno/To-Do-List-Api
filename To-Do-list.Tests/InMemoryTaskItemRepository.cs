using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using To_Do_List.Domain.Entities;
using To_Do_List.Domain.Interfaces;

namespace To_Do_List.Infrastructure.Repositories
{
    public class InMemoryTaskItemRepository : ITaskItemRepository
    {
        private readonly List<TaskItem> _taskItems = new();

        public Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            var result = _taskItems.OrderBy(task => task.CreatedAt);
            return Task.FromResult(result.AsEnumerable());
        }

        public Task<TaskItem> GetByIdAsync(Guid id)
        {
            var taskItem = _taskItems.FirstOrDefault(task => task.Id == id);
            return Task.FromResult(taskItem);
        }

        public Task AddAsync(TaskItem taskItem)
        {
            taskItem.Id = Guid.NewGuid();
            taskItem.CreatedAt = DateTime.UtcNow;
            _taskItems.Add(taskItem);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(TaskItem taskItem)
        {
            var taskItemToUpdate = _taskItems.FirstOrDefault(task => task.Id == taskItem.Id);
            if (taskItemToUpdate == null)
            {
                throw new ArgumentException("Task not found for task id: " + taskItem.Id);
            }

            var properties = typeof(TaskItem).GetProperties();
            foreach (var property in properties)
            {
                if (property.Name != "CreatedAt")
                {
                    var newValue = property.GetValue(taskItem);
                    var originalValue = property.GetValue(taskItemToUpdate);

                    if (newValue != null && !(newValue is string str && string.IsNullOrEmpty(str)))
                    {
                        property.SetValue(taskItemToUpdate, newValue);
                    }
                }
            }

            taskItemToUpdate.UpdatedAt = DateTime.UtcNow;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            var taskItem = _taskItems.FirstOrDefault(task => task.Id == id);
            if (taskItem == null)
            {
                throw new ArgumentException("Task not found for task id: " + id);
            }

            _taskItems.Remove(taskItem);
            return Task.CompletedTask;
        }
    }
}
