using Chess.Users.Models;
using Chess.Users.Services.EntityServices.Interfaces;
using Chess.Users.Services.Interfaces;
using Chess.Users.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Chess.Users.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        public AuthController(ITokenService tokenService,
            IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var userModel = await _userService.GetByEmailAsync(loginModel.Email);

            if (userModel == default)
            {
                return BadRequest();
            }
            if (!PasswordHasher.VerifyPassword(loginModel.Password, userModel.Password))
            {
                return NotFound();
            }

            var token = _tokenService.GenerateJWT(userModel);

            return Ok(token);
        }
    }
}
