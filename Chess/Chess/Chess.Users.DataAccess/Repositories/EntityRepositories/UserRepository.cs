using Chess.Users.DataAccess.Entities;
using Chess.Users.DataAccess.Repositories.EntityRepositories.Interfaces;
using Chess.Users.Models.UserModels;
using Chess.Users.Models.UserModels.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Chess.Users.DataAccess.Repositories.EntityRepositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbSet<User> dbSet) 
            : base(dbSet) { }

        public async Task<User> GetByEmailAsync(string email)
            => await _dbSet.SingleOrDefaultAsync(u => u.Email == email); 
        
        public async Task<IUserDetailsModel> GetUserDetailsAsync(Guid userId)
        {
            var userDetails = await _dbSet
                .Where(u => u.Id == userId)
                .Select(u => new UserDetailsModel
                { 
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Points = u.Points,
                    Username = u.Username
                }).SingleOrDefaultAsync();

            return userDetails;
        }
    }
}