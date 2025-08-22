using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoAPI.DTOs;

namespace TodoAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
    }
}
