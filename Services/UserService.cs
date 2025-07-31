using Microsoft.EntityFrameworkCore;
using movie_backend.DataAccess;
using movie_backend.DTOs;
using movie_backend.Models;

namespace movie_backend.Services
{
    public class UserService
    {
        private readonly ApplicationDBContext _context;


        public UserService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, string? ErrorMessage)> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email || u.Username == dto.Username);

            if (existingUser != null)
            {
                if (existingUser.Email == dto.Email)
                {
                    return (false, "Email already in use");
                }
                if (existingUser.Username == dto.Username)
                {
                    return (false, "Username already in use");
                }
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = passwordHash
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return (true, null);
        }

        public async Task<(bool Success, string? Token, string? ErrorMessage)> AuthenticateAsync(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Identifier || u.Username == dto.Identifier);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                return (false, null, "Invalid credentials");
            }

            return (true, "", null);
        }


    }
}
