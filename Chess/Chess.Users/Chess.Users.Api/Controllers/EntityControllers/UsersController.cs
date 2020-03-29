using Chess.Users.Api.Controllers;
using Chess.Users.DataAccess.Entities;
using Chess.Users.DataAccess.Repositories.EntityRepositories;
using Chess.Users.Models.EntityModels.UserModels;
using Chess.Users.Services.EntityServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chess.UsersService.Controllers.EntityControllers
{
    [ApiController]
    [Route("users")]
    public class UsersController 
        : BaseCrudController<IUserService, UserModel, User, UserRepository>
    {
        public UsersController(IUserService service) 
            : base(service) { }
    }
}
