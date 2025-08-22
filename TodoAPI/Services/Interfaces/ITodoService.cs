using TodoAPI.DTOs;

namespace TodoAPI.Services.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItemReadDto>> GetTodosAsync(int userId, int pageNumber, int pageSize);
        Task<TodoItemReadDto> GetTodoByIdAsync(int id, int userId);
        Task<TodoItemReadDto> CreateTodoAsync(TodoItemCreateDto dto, int userId);
        Task<TodoItemReadDto> UpdateTodoAsync(int id, TodoItemUpdateDto dto, int userId);
        Task <string> DeleteTodoAsync(int id, int userId);
    }
}
