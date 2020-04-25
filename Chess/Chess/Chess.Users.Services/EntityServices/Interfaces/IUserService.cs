using Chess.Users.DataAccess.Entities;
using Chess.Users.DataAccess.Repositories.EntityRepositories;
using Chess.Users.Models.EntityModels.UserModels;
using Chess.Users.Models.EntityModels.UserModels.Interfaces;
using System;
using System.Threading.Tasks;

namespace Chess.Users.Services.EntityServices.Interfaces
{
    public interface IUserService : IBaseEntityService<User, UserModel, UserRepository>
    {
        /// <summary>
        /// Gets a user model by a given email.
        /// </summary>
        /// <param name="email">An email of an exsiting user</param>
        /// <returns>Returns the user with the given email.</returns>
        Task<IUserModel> GetByEmailAsync(string email);

        /// <summary>
        /// Checks if a user if this email already exists.
        /// </summary>
        /// <param name="email">The email of the new user.</param>
        /// <returns>True of False depending on whether a user with this email already exists.</returns>
        Task<bool> DoesUserExistAsync(string email);

        /// <summary>
        /// Send a password change email
        /// </summary>
        /// <param name="userId">User which requested the password change.</param>
        Task RequestPasswordChange(Guid userId);
    }
}
