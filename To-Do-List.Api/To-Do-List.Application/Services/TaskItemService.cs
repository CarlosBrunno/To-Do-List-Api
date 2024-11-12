using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List.Domain.Entities;
using To_Do_List.Domain.Interfaces;

namespace To_Do_List.Application.Services
{
    public class TaskItemService
    {
        private readonly ITaskItemRepository _taskitemRepository;

        public TaskItemService(ITaskItemRepository taskitemRepository)
        {
            _taskitemRepository = taskitemRepository;
        }

        public Task<IEnumerable<TaskItem>> GetAllAsync() => _taskitemRepository.GetAllAsync();
        public Task<TaskItem> GetByIdAsync(Guid id) => _taskitemRepository.GetByIdAsync(id);
        public Task AddAsync(TaskItem taskitem) => _taskitemRepository.AddAsync(taskitem);
        public Task UpdateAsync(TaskItem taskitem) => _taskitemRepository.UpdateAsync(taskitem);
        public Task DeleteAsync(Guid id) => _taskitemRepository.DeleteAsync(id);
    }
}
