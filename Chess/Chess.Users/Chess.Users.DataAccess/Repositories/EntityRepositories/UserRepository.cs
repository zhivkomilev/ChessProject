using Chess.Users.DataAccess.Entities;
using Chess.Users.DataAccess.Repositories.EntityRepositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Chess.Users.DataAccess.Repositories.EntityRepositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbSet<User> dbSet) 
            : base(dbSet) { }

        public async Task<User> GetByUsernameAsync(string username)
            => await _dbSet.SingleOrDefaultAsync(u => u.Username == username);   
    }
}