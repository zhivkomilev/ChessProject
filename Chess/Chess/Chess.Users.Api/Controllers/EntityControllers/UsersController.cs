using Chess.Users.Api.Controllers;
using Chess.Users.DataAccess.Entities;
using Chess.Users.DataAccess.Repositories.EntityRepositories;
using Chess.Users.Models.EntityModels.UserModels;
using Chess.Users.Services.EntityServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    }
}