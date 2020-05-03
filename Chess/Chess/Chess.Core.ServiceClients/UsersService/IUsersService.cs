using Chess.Users.Models.UserModels;
using Refit;
using System;
using System.Threading.Tasks;

namespace Chess.ApiGateway.Api.ApiServices.UsersService
{
    public interface IUsersService
    {
        [Post("/register")]
        Task<string> Register(RegisterModel model);

        [Post("/change-password")]
        Task ChangePassword(ChangePasswordModel model);

        [Get("/details")]
        Task<string> Details(Guid userId);

        [Post("/update-user")]
        Task UpdateDetails(UserDetailsModel model);

        [Post("/update-points")]
        Task UpdatePoints(PointsUpdateModel model);

        [Get("/get-all-users")]
        Task<string> GetAllUserDetails();
    }
}