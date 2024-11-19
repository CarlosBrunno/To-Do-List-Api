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
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetAll()
        {
            try
            {
                var taskItems = await _taskitemService.GetAllAsync();
                return Ok(taskItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetById(Guid id)
        {
            var taskitem = await _taskitemService.GetByIdAsync(id);
            if (taskitem == null)
            {
                return NotFound();
            }
            return Ok(taskitem);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskItemDto taskitemDto)
        {
            try
            {
                var taskItem = new TaskItem(taskitemDto);
                var validator = new TaskItemValidator();
                var result = validator.Validate(taskItem);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }

                await _taskitemService.AddAsync(taskItem);
                return Ok(taskItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TaskItemDto taskitemDto)
        {
            try
            {
                taskitemDto.Id = id;
                taskitemDto.UpdatedAt = DateTime.UtcNow;

                var taskItem = new TaskItem(taskitemDto);
                await _taskitemService.UpdateAsync(taskItem);
                return Ok("Task of id "+id+" updated!");
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _taskitemService.DeleteAsync(id);
                return Ok("Task of id " + id + " deleted!");

            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

    }
}
