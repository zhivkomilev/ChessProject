using Chess.Users.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chess.Users.DataAccess
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
