using Chess.Users.Models.EntityModels.UserModels;
using Chess.Users.Models.EntityModels.UserModels.Interfaces;
using System.Threading.Tasks;

namespace Chess.Users.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateJwtAsync(IUserModel userInfo);
    }
}
