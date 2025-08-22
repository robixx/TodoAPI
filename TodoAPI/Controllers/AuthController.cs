using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.DTOs;
using TodoAPI.Services.Interfaces;

namespace TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            try
            {
                await _authService.RegisterAsync(dto);
                return Ok(new { message = "User registered successfully", status = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                var result = await _authService.LoginAsync(dto);
                return Ok(result); // AccessToken + ExpiresAt
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message, status = false });
            }
            catch (Exception ex)
            {
                // Optional: catch all other exceptions
                return BadRequest(new { message = ex.Message, status = false });
            }
        }
    }
}
