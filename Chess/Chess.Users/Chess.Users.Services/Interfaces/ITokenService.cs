using Chess.Users.Models.EntityModels.UserModels;
using Chess.Users.Models.EntityModels.UserModels.Interfaces;

namespace Chess.Users.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateJWT(IUserModel userInfo);
    }
}
