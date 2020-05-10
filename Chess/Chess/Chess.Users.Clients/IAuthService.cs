using Chess.Users.Models.UserModels;
using Refit;
using System.Threading.Tasks;

namespace Chess.Users.Clients
{
    public interface IAuthService
    {
        [Post("/")]
        Task<string> Post(UserLoginModel model);
    }
}