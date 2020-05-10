using Chess.Users.Models.UserModels;
using System.Threading.Tasks;

namespace Chess.Users.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateJwtAsync(UserModel userInfo);
    }
}
