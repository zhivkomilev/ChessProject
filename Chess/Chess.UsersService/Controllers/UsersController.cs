using Chess.UsersService.Data;
using Chess.UsersService.DataAccess;
using Chess.UsersService.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var uow = new UnitOfWork(new UsersDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<UsersDbContext>()));

            var repo = await uow.RepositoryAsync<UserRepository>();

            return Ok();
        }
    }
}
