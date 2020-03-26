using Chess.UsersService.Data;
using Chess.UsersService.Data.Entities;

namespace Chess.UsersService.DataAccess.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(UsersDbContext dbContext) 
            : base(dbContext)
        {
        }
    }
}
