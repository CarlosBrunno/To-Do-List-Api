using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using To_Do_List.Domain.Entities;
using To_Do_List.Infrastructure.Data;

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
            await _context.TaskItems.ToListAsync();

        public async Task<TaskItem> GetByIdAsync(Guid id) =>
            await _context.TaskItems.FindAsync(id);

        public async Task AddAsync(TaskItem taskItem)
        {
            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaskItem taskItem)
        {
            _context.Entry(taskItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem != null)
            {
                _context.TaskItems.Remove(taskItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
