using AutoMapper;
using TodoAPI.DTOs;
using TodoAPI.Models;

namespace TodoAPI.Mappings
{
    public class AutoMapperProfile :Profile
    {
        public AutoMapperProfile()
        {
            // Map RegisterDto → ApplicationUser
            CreateMap<RegisterDto, ApplicationUser>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
            CreateMap<ApplicationUser, AuthResponseDto>();
            CreateMap<TodoItemCreateDto, TodoItem>();
            CreateMap<TodoItemUpdateDto, TodoItem>();
            CreateMap<TodoItem, TodoItemReadDto>();
        }
    }
}
