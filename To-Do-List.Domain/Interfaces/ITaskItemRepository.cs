using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List.Domain.Entities;

namespace To_Do_List.Domain.Interfaces
{
    public interface ITaskItemRepository
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<TaskItem> GetByIdAsync(Guid id);
        Task AddAsync(TaskItem product);
        Task UpdateAsync(TaskItem product);
        Task DeleteAsync(Guid id);
    }
}
