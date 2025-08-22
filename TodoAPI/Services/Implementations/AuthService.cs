using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoAPI.Data;
using TodoAPI.DTOs;
using TodoAPI.Models;
using TodoAPI.Services.Interfaces;

namespace TodoAPI.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthService(AppDbContext context, IConfiguration config, IMapper mapper)
        {
            _context = context;
            _config = config;
            _mapper = mapper;
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
           
            // Check if email exists
            if (await _context.ApplicationUser.AnyAsync(u => u.Email == dto.Email))
                throw new Exception("Email already registered");

            // Map DTO → Entity
            var user = _mapper.Map<ApplicationUser>(dto);

            // Hash password
            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            _context.ApplicationUser.Add(user);
            await _context.SaveChangesAsync();           


        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _context.ApplicationUser.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                throw new Exception("Invalid Email and Password");

            return new AuthResponseDto
            {
                Token = GenerateToken(user),
                ExpiresAt = DateTime.Now.AddMinutes(30)
            };
        }

        private string GenerateToken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
