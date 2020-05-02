using Chess.Users.Models.UserModels.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Chess.Users.Models.UserModels
{
    public class UserModel : BaseModel, IUserModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public int Points { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
