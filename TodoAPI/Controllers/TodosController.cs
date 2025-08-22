using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TodoAPI.DTOs;
using TodoAPI.Services.Interfaces;

namespace TodoAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
   
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodosController(ITodoService service)
        {
            _service = service;
        }

        private int GetUserId()
        {
            var subClaim = User.FindFirstValue("UserId")
                           ?? throw new UnauthorizedAccessException("User ID claim not found.");
            return int.Parse(subClaim);
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            int id = GetUserId();
            var todos = await _service.GetTodosAsync(id, pageNumber, pageSize);
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodo(int id)
        {
            var todo = await _service.GetTodoByIdAsync(id, GetUserId());
            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo(TodoItemCreateDto dto)
        {
            var todo = await _service.CreateTodoAsync(dto, GetUserId());
            return Ok(todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, TodoItemUpdateDto dto)
        {
            var todo = await _service.UpdateTodoAsync(id, dto, GetUserId());
            return Ok(todo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var message = await _service.DeleteTodoAsync(id, GetUserId());
            return Ok(new { message });
        }
    }
}
