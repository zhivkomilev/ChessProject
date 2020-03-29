using Chess.Users.Models.EntityModels.UserModels.Interfaces;
using System;

namespace Chess.Users.Models.EntityModels.UserModels
{
    public class UserModel : BaseModel, IUserModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
