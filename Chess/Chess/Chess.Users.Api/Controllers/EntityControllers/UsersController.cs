using Chess.Users.Api.Controllers;
using Chess.Users.DataAccess.Entities;
using Chess.Users.DataAccess.Repositories.EntityRepositories;
using Chess.Users.Models.EntityModels.UserModels;
using Chess.Users.Services.EntityServices.Interfaces;
using Chess.Users.Services.Exceptions;
using Chess.Users.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Chess.UsersService.Controllers.EntityControllers
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
            {
                return BadRequest($"Email already exists.");
            }

            return await base.Insert(model);
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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }
    }
}