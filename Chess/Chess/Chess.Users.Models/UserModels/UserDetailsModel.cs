using Chess.Users.Models.UserModels.Interfaces;

namespace Chess.Users.Models.UserModels
{
    public class UserDetailsModel : IUserDetailsModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public int Points { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
