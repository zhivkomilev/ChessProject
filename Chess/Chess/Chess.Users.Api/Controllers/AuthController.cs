using Chess.Users.Models;
using Chess.Users.Services.EntityServices.Interfaces;
using Chess.Users.Services.Interfaces;
using Chess.Users.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Chess.Users.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ITokenService tokenService,
            IUserService userService,
            ILogger<AuthController> logger)
        {
            _tokenService = tokenService;
            _userService = userService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            try
            {
                var userModel = await _userService.GetByEmailAsync(loginModel.Email);
                if (userModel == default)
                    return NotFound($"No user with email:{loginModel.Email} found.");
                
                if (!PasswordHasher.VerifyPassword(loginModel.Password, userModel.Password))
                    return StatusCode(401, $"Wrong password.");
                
                var token = _tokenService.GenerateJWT(userModel);

                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong while login was attempted.");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
