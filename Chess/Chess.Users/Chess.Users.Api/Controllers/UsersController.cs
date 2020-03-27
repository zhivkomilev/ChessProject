using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Chess.UsersService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Test()
        {
            // Used for testing
            return Ok();
        }
    }
}
