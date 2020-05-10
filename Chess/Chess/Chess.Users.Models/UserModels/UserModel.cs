using Chess.Core.Models;

namespace Chess.Users.Models.UserModels
{
    public class UserModel : BaseModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public int Points { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}