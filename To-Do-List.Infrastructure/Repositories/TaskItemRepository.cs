using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using To_Do_List.Domain.Entities;
using To_Do_List.Infrastructure.Data;
using To_Do_List.Application.Services;

namespace To_Do_List.Infrastructure.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync() =>
        await _context.TaskItems
            .OrderBy(task => task.CreatedAt)
            .ToListAsync();


        public async Task<TaskItem> GetByIdAsync(Guid id) =>
            await _context.TaskItems.FindAsync(id);

        public async Task AddAsync(TaskItem taskItem)
        {
            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskItem taskItem)
        {
            var taskItemToUpdate = await GetByIdAsync(taskItem.Id);
            if (taskItemToUpdate == null)
            {
                throw new ArgumentException("Task not found for task id: " + taskItem.Id);
            }

            var properties = typeof(TaskItem).GetProperties();
            foreach (var property in properties)
            {
                
                if(property.Name != "CreatedAt")
                {
                    var newValue = property.GetValue(taskItem);
                    var originalValue = property.GetValue(taskItemToUpdate);

                    if (newValue != null && !(newValue is string str && string.IsNullOrEmpty(str)))
                    {
                        property.SetValue(taskItemToUpdate, newValue);
                    }
                }
                
            }
            await _context.SaveChangesAsync();
        }



        public async Task DeleteAsync(Guid id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem == null)
            {
                throw new ArgumentException("Task not found for task id: " + id);
            }

            _context.TaskItems.Remove(taskItem);
            await _context.SaveChangesAsync();
        }

    }
}
