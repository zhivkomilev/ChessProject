using Chess.Users.DataAccess.Entities;
using Chess.Users.DataAccess.Repositories.Interfaces;
using Chess.Users.Models.UserModels;
using Chess.Users.Models.UserModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chess.Users.DataAccess.Repositories.EntityRepositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmailAsync(string username);

        Task<IUserDetailsModel> GetUserDetailsAsync(Guid userId);

        Task<IEnumerable<IUserDetailsModel>> GetAllUserDetailsAsync();
    }
}
