using Chess.Users.Models.UserModels.Interfaces;
using System.Threading.Tasks;

namespace Chess.Users.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateJwtAsync(IUserModel userInfo);
    }
}
