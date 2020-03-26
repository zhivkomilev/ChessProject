using Chess.UsersService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chess.UsersService.Data
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options) { }


        public DbSet<User> Users { get; set; }
    }
}
