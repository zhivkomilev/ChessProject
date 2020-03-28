using Chess.Users.Models.EntityModels.UserModels;
using Chess.Users.Services.EntityServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Chess.UsersService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            // Used for testing
            return Ok();
        }
    }
}
