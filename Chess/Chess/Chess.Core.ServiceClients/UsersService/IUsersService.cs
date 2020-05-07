using Chess.Users.Models.UserModels;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chess.ApiGateway.Api.ApiServices.UsersService
{
    public interface IUsersService
    {
        [Post("/register")]
        Task<UserDetailsModel> Register(RegisterModel model);

        [Post("/change-password/{userId}")]
        Task ChangePassword([AliasAs("userId")] Guid userId, ChangePasswordModel model);

        [Get("/details/{userId}")]
        Task<UserDetailsModel> Details([AliasAs("userId")] Guid userId);

        [Patch("/update-user/{userId}")]
        Task UpdateDetails([AliasAs("userId")] Guid userId, UserDetailsModel model);

        [Patch("/update-points/{userId}")]
        Task UpdatePoints([AliasAs("userId")] Guid userId, PointsUpdateModel model);

        [Get("/get-all-users")]
        Task<IEnumerable<UserDetailsModel>> GetAllUserDetails();
    }
}