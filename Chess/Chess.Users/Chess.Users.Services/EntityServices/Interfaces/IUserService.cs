using Chess.Users.DataAccess.Entities;
using Chess.Users.DataAccess.Repositories.EntityRepositories;
using Chess.Users.Models.EntityModels.UserModels;

namespace Chess.Users.Services.EntityServices.Interfaces
{
    public interface IUserService : IBaseEntityService<User, UserModel, UserRepository>
    {
    }
}
