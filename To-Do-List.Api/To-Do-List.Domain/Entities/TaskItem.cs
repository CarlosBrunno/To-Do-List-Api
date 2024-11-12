using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List.Domain.Entities
{
    public class TaskItem : EntityBase
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsChecked { get; set; }
    }
}
