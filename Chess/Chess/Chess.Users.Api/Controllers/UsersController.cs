using Chess.Users.Api.Controllers;
using Chess.Users.DataAccess.Entities;
using Chess.Users.DataAccess.Repositories.EntityRepositories;
using Chess.Users.Models.UserModels;
using Chess.Users.Services.EntityServices.Interfaces;
using Chess.Users.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Chess.UsersService.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController
        : BaseCrudController<IUserService, UserModel, User, UserRepository>
    {
        public UsersController(IUserService service,
            ILogger<UsersController> logger)
            : base(service, logger) { }

        [HttpPost("register")]
        public override async Task<IActionResult> Insert(UserModel model)
        {
            if (await _service.DoesUserExistAsync(model.Email))
                return BadRequest($"Email already exists.");

            await base.Insert(model);
            return Ok();
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (model == null)
                return BadRequest();

            try
            {
                await _service.ChangePasswordAsync(model);
                await _service.SaveChangesAsync();

                return Ok();
            }
            catch(ChangePasswordException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("details")]
        public async Task<IActionResult> Details(Guid userId)
        {
            if (userId == default)
                return BadRequest();

            var userDetails = await _service.GetUserDetailsAsync(userId);

            if (userDetails == default)
                return NotFound();

            return Ok(userDetails);
        }


        [HttpPost("update-user")]
        public async Task<IActionResult> UpdateDetails(UserDetailsModel model)
        {
            if (model == default)
                return BadRequest();

            await _service.UpdateDetailsAsync(model);
            await _service.SaveChangesAsync();

            return Ok();
        }
    }
}