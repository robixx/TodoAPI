using AutoMapper;
using TodoAPI.DTOs;
using TodoAPI.Models;
using TodoAPI.Repositories;
using TodoAPI.Services.Interfaces;

namespace TodoAPI.Services.Implementations
{
    public class TodoService : ITodoService
    {
        private readonly IGenericRepository<TodoItem> _repo;
        private readonly IMapper _mapper;

        public TodoService(IGenericRepository<TodoItem> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<TodoItemReadDto> CreateTodoAsync(TodoItemCreateDto dto, int userId)
        {
            var todo = _mapper.Map<TodoItem>(dto);
            todo.UserId = userId;
            await _repo.AddAsync(todo);
            return _mapper.Map<TodoItemReadDto>(todo);
        }

        public async Task<string> DeleteTodoAsync(int id, int userId)
        {
            var todo = await _repo.GetByIdAsync(id, userId);
            if (todo == null) throw new KeyNotFoundException("Data not found");
            await _repo.DeleteAsync(todo);
            return "Todo data Delete Successfully";
        }

        public async Task<TodoItemReadDto> GetTodoByIdAsync(int id, int userId)
        {
            var todo = await _repo.GetByIdAsync(id, userId);
            if (todo == null) throw new KeyNotFoundException("Todo not found");
            return _mapper.Map<TodoItemReadDto>(todo);
        }

        public async Task<PagedResultDto<TodoItemReadDto>> GetTodosAsync(int userId, int pageNumber, int pageSize)
        {
            var (todos, totalCount) = await _repo.GetAllAsync(userId, pageNumber, pageSize);
            var todoDtos = _mapper.Map<IEnumerable<TodoItemReadDto>>(todos);

            return new PagedResultDto<TodoItemReadDto>
            {
                Items = todoDtos,
                Page = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<TodoItemReadDto> UpdateTodoAsync(int id, TodoItemUpdateDto dto, int userId)
        {
            var todo = await _repo.GetByIdAsync(id, userId);
            if (todo == null) throw new KeyNotFoundException("Todo not found");

            _mapper.Map(dto, todo);
            await _repo.UpdateAsync(todo);
            return _mapper.Map<TodoItemReadDto>(todo);
        }
    }
}
