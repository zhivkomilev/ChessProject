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

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var response = await _usersService.Register(model);

            return Created(Request.Path.Value, response);
        }

        [HttpPost("change-password/{userId}")]
        public async Task<IActionResult> ChangePassword(Guid userId, [FromBody] ChangePasswordModel model)
        {
            await _usersService.ChangePassword(userId, model);

            return Ok();
        }

        [HttpGet("details/{userId}")]
        public async Task<IActionResult> Details(Guid userId)
        {
            var response = await _usersService.Details(userId);

            return Ok(response);
        }

        [HttpPost("update-user/{userId}")]
        public async Task<IActionResult> UpdateDetails(Guid userId, [FromBody] UserDetailsModel model)
        {
            await _usersService.UpdateDetails(userId, model);

            return Ok();
        }

        [HttpPost("update-points/{userId}")]
        public async Task<IActionResult> UpdatePoints(Guid userId, [FromBody] PointsUpdateModel model)
        {
            await _usersService.UpdatePoints(userId, model);

            return Ok();
        }

        [HttpPost("get-all-users")]
        public async Task<IActionResult> GetAllUserDetails()
        {
            var response = await _usersService.GetAllUserDetails();

            return Ok(response);
        }
    }
}