using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using To_Do_List.Application.Services;
using To_Do_List.Domain.Contracts;
using To_Do_List.Domain.Dto;
using To_Do_List.Domain.Entities;

namespace TaskItemsApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        private readonly TaskItemService _taskitemService;

        public TaskItemController(TaskItemService taskitemService)
        {
            _taskitemService = taskitemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetAll() => Ok(await _taskitemService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetById(Guid id)
        {
            var taskitem = await _taskitemService.GetByIdAsync(id);
            return taskitem == null ? NotFound() : Ok(taskitem);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskItemDto taskitemDto)
        {
            try
            {
                var taskItem = new TaskItem(taskitemDto);

                // Validator before saving
                var validator = new TaskItemValidator();
                var result = validator.Validate(taskItem);

                if (!result.IsValid)
                {
                    // Returns validation errors
                    return BadRequest(result.Errors);
                }

                await _taskitemService.AddAsync(taskItem);
                return Ok(taskItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }

           // return CreatedAtAction(nameof(GetById), new { id = taskItem.Id }, taskItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TaskItem taskitem)
        {
            if (id != taskitem.Id) return BadRequest();
            await _taskitemService.UpdateAsync(taskitem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _taskitemService.DeleteAsync(id);
            return NoContent();
        }

    }
}
