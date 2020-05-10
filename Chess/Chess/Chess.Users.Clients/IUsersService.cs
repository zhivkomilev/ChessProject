using Chess.Users.Models.UserModels;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chess.Users.Clients
{
    public interface IUsersService
    {
        [Post("")]
        Task<UserDetailsModel> Post(UserRegisterModel model);

        [Post("/change-password/{userId}")]
        Task ChangePassword([AliasAs("userId")] Guid userId, ChangePasswordModel model);

        [Get("/details/{userId}")]
        Task<UserDetailsModel> Details([AliasAs("userId")] Guid userId);

        [Patch("/details/{userId}")]
        Task Details([AliasAs("userId")] Guid userId, UserDetailsModel model);

        [Patch("/points/{userId}")]
        Task Points([AliasAs("userId")] Guid userId, PointsUpdateModel model);

        [Get("")]
        Task<IEnumerable<UserDetailsModel>> GetAll();
    }
}