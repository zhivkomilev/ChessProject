using Chess.ApiGateway.Api.ApiServices.UsersService;
using Chess.Users.Models.UserModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Chess.ApiGateway.Api.Controllers.UsersService
{
    [ApiController]
    [Route("/users")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _usersService.GetById(id);

            return Ok(response);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var response = await _usersService.Register(model);

            return Created(Request.Path.Value, response);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            await _usersService.ChangePassword(model);

            return Ok();
        }

        [HttpGet("details")]
        public async Task<IActionResult> Details(Guid userId)
        {
            var response = await _usersService.Details(userId);

            return Ok(response);
        }

        [HttpPost("update-user")]
        public async Task<IActionResult> UpdateDetails(UserDetailsModel model)
        {
            await _usersService.UpdateDetails(model);

            return Ok();
        }
    }
}