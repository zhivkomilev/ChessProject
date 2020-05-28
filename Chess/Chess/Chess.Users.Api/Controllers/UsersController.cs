using Chess.Core.Api.Controllers;
using Chess.Core.Middlewares.Models;
using Chess.Users.DataAccess.Entities;
using Chess.Users.Models.UserModels;
using Chess.Users.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Chess.UsersService.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController
        : BaseCrudController<IUserService, UserModel, User>
    {
        public UsersController(IUserService service,
            ILogger<UsersController> logger)
            : base(service, logger) { }


        [HttpPost("change-password/{userId}")]
        public async Task<IActionResult> ChangePassword(Guid userId, [FromBody] ChangePasswordModel model)
        {
            var response = await _service.ChangePasswordAsync(userId, model);
            if (!response.IsSuccessful)
                return ProcessResponse(response);

            await _service.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("details/{userId}")]
        public async Task<IActionResult> Details(Guid userId)
        {
            var response = await _service.GetUserDetailsAsync(userId);

            return ProcessResponse(response);
        }

        [HttpPatch("details/{userId}")]
        public async Task<IActionResult> Details(Guid userId, [FromBody] UserDetailsModel model)
        {
            var response = await _service.UpdateDetailsAsync(userId, model);
            if (!response.IsSuccessful)
                return ProcessResponse(response);
            
            await _service.SaveChangesAsync();
            return ProcessResponse(response);
        }

        [HttpPatch("points/{userId}")]
        public async Task<IActionResult> Points(Guid userId, [FromBody]PointsUpdateModel model)
        {
            var response = await _service.UpdatePointsAsync(userId, model);
            if (!response.IsSuccessful)
                return ProcessResponse(response);

            await _service.SaveChangesAsync();
            return ProcessResponse(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllUserDetailsAsync();
            
            return ProcessResponse(response);
        }
    }
}