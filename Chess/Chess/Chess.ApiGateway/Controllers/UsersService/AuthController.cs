using Chess.ApiGateway.Api.ApiServices.UsersService;
using Chess.Core.Middlewares.Models;
using Chess.Users.Models.UserModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Chess.ApiGateway.Api.Controllers.UsersService
{
    [ApiController]
    [Route("/auth")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ErrorModel), 401)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var response = await _authService.Login(model);

            return Ok(response);
        }
    }
}