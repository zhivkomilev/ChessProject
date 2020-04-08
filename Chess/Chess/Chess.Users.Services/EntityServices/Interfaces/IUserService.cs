using Chess.Users.DataAccess.Entities;
using Chess.Users.DataAccess.Repositories.EntityRepositories;
using Chess.Users.Models.EntityModels.UserModels;
using Chess.Users.Models.EntityModels.UserModels.Interfaces;
using System.Threading.Tasks;

namespace Chess.Users.Services.EntityServices.Interfaces
{
    public interface IUserService : IBaseEntityService<User, UserModel, UserRepository>
    {
        Task<IUserModel> GetByEmailAsync(string email);
    }
}
