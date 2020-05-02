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

        [Get("/get")]
        Task<string> GetById(Guid id);

        [Post("/change-password")]
        Task ChangePassword(ChangePasswordModel model);

        [Get("/details")]
        Task<string> Details(Guid userId);

        [Post("/update-user")]
        Task UpdateDetails(UserDetailsModel model);
    }
}
