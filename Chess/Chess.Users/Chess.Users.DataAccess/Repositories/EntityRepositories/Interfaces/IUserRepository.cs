using Chess.Users.DataAccess.Entities;
using Chess.Users.DataAccess.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Chess.Users.DataAccess.Repositories.EntityRepositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
    }
}
