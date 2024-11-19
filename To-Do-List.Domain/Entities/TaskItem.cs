using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_List.Domain.Dto;

namespace To_Do_List.Domain.Entities
{
    public class TaskItem : EntityBase
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsChecked { get; set; }
    

    //Constructor
    public TaskItem(TaskItemDto taskItemDto)
    {
        Id = taskItemDto.Id.HasValue ? taskItemDto.Id.Value : new Guid(Guid.NewGuid().ToString());
        Title = taskItemDto.Title;
        Description = taskItemDto.Description;
        IsChecked = taskItemDto.IsChecked.HasValue ? taskItemDto.IsChecked.Value : false;
        CreatedAt = taskItemDto.CreatedAt.HasValue ? taskItemDto.CreatedAt.Value : DateTime.UtcNow;
        UpdatedAt = taskItemDto.UpdatedAt;
    }

        public TaskItem() { }

    }
}
