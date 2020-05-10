using Chess.Users.Clients;
using Chess.Users.Models.UserModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(UserRegisterModel model)
        {
            var response = await _usersService.Post(model);

            return Created(Request.Path.Value, response);
        }

        [HttpPost("change-password/{userId}")]
        public async Task<IActionResult> ChangePassword(Guid userId, [FromBody] ChangePasswordModel model)
        {
            await _usersService.ChangePassword(userId, model);

            return Ok();
        }

        [HttpGet("details/{userId}")]
        [ProducesResponseType(typeof(UserDetailsModel), 200)]
        public async Task<IActionResult> Details(Guid userId)
        {
            var response = await _usersService.Details(userId);

            return Ok(response);
        }

        [HttpPatch("details/{userId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Details(Guid userId, [FromBody] UserDetailsModel model)
        {
            await _usersService.Details(userId, model);

            return Ok();
        }

        [HttpPatch("points/{userId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Points(Guid userId, [FromBody] PointsUpdateModel model)
        {
            await _usersService.Points(userId, model);

            return Ok();
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<UserDetailsModel>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _usersService.GetAll();

            return Ok(response);
        }
    }
}