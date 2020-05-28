using Chess.Core.Api.Controllers;
using Chess.Core.Middlewares.Models;
using Chess.Users.Models.UserModels;
using Chess.Users.Services;
using Chess.Users.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Chess.Users.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(ErrorModel), 401)]
        [ProducesResponseType(typeof(ErrorModel), 404)]
        public async Task<IActionResult> Post(UserLoginModel loginModel)
        {
                var response = await _userService.Login(loginModel);
            
                return ProcessResponse(response);
        }
    }
}