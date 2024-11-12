using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List.Domain.Dto
{
    public class TaskItemDto : EntityBaseDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsChecked { get; set; }
    }
}
