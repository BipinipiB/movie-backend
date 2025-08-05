using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using movie_backend.DataAccess;
using movie_backend.DTOs;
using movie_backend.Models;
using movie_backend.Services;

namespace movie_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly ApplicationDBContext _context;
        private readonly UserService _userService;

        public AuthController(ApplicationDBContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        //Register User Endpoint
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {

            var validationResult = await _userService.RegisterAsync(dto);

            if(validationResult.Success)
            {
                //return Ok("User registered successfully");

                return Ok(new
                {
                    message = "User registered successfully",
                 
                });
            }
            else
            {
               // return BadRequest(validationResult.ErrorMessage);

                return BadRequest(new
                {
                    message = validationResult.ErrorMessage,

                });
            }

        }

        //Login User Endpoint
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var validationResult = await _userService.AuthenticateAsync(dto);
            
            Console.WriteLine("Identifier: " + dto.Identifier);
            Console.WriteLine("Password: " + dto.Password);


            if (!validationResult.Success)
            {
                return Unauthorized(validationResult.ErrorMessage);
            }
            else
            {
                return Ok(new
                {
                    message = "Login Successfulk",
                    code = 200
                });
                
            }

        }

    }
}
