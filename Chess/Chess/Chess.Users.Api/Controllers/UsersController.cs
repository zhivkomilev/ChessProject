using Chess.Core.Middlewares.Models;
using Chess.Users.Api.Controllers;
using Chess.Users.DataAccess.Entities;
using Chess.Users.DataAccess.Repositories.EntityRepositories;
using Chess.Users.Models.UserModels;
using Chess.Users.Services.EntityServices.Interfaces;
using Chess.Users.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chess.UsersService.Controllers
{
    [ApiController]
    [Route("api/users")]
    [ProducesResponseType(typeof(ErrorModel), 400)]
    [ProducesResponseType(typeof(ErrorModel), 404)]
    public class UsersController
        : BaseCrudController<IUserService, UserModel, User, UserRepository>
    {
        public UsersController(IUserService service,
            ILogger<UsersController> logger)
            : base(service, logger) { }

        [HttpPost("register")]
        [ProducesResponseType(typeof(UserModel), 200)]
        public override async Task<IActionResult> Insert(UserModel model)
        {
            if (await _service.DoesUserExistAsync(model.Email))
                return BadRequest(new ErrorModel
                {
                    Message = $"Email already exists.",
                    StatusCode = 400
                });

            await base.Insert(model);
            return Ok();
        }

        [HttpPost("change-password/{userId}")]
        public async Task<IActionResult> ChangePassword(Guid userId, [FromBody] ChangePasswordModel model)
        {
            if (model == null)
                return BadRequest(new ErrorModel
                {
                    Message = "Invalid model passed",
                    StatusCode = 400
                });

            try
            {
                await _service.ChangePasswordAsync(userId, model);
                await _service.SaveChangesAsync();

                return Ok();
            }
            catch (ChangePasswordException ex)
            {
                return BadRequest(new ErrorModel
                {
                    Message = ex.Message,
                    StatusCode = 400
                });
            }
        }

        [HttpGet("details/{userId}")]
        [ProducesResponseType(typeof(UserDetailsModel), 200)]
        public async Task<IActionResult> Details(Guid userId)
        {
            if (userId == default)
                return BadRequest(new ErrorModel
                {
                    Message = "Invalid id passed",
                    StatusCode = 400
                });

            var userDetails = await _service.GetUserDetailsAsync(userId);

            if (userDetails == default)
                return NotFound(new ErrorModel
                {
                    Message = $"Model with Id {userId} not found",
                    StatusCode = 400
                });

            return Ok(userDetails);
        }

        [HttpPatch("update-user/{userId}")]
        public async Task<IActionResult> UpdateDetails(Guid userId, [FromBody] UserDetailsModel model)
        {
            if (model == default)
                return BadRequest(new ErrorModel
                {
                    Message = "Invalid model passed",
                    StatusCode = 400
                });

            await _service.UpdateDetailsAsync(userId, model);
            await _service.SaveChangesAsync();

            return Ok();
        }

        [HttpPatch("update-points/{userId}")]
        public async Task<IActionResult> UpdatePoints(Guid userId, [FromBody]PointsUpdateModel model)
        {
            if (model == null)
                return BadRequest(new ErrorModel
                {
                    Message = "Invalid model passed",
                    StatusCode = 400
                });

            await _service.UpdatePointsAsync(userId, model);
            await _service.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("get-all-users")]
        [ProducesResponseType(typeof(IEnumerable<UserDetailsModel>), 200)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _service.GetAllUserDetailsAsync();

            return Ok(users);
        }
    }
}