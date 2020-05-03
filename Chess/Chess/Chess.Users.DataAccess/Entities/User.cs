using System.ComponentModel.DataAnnotations;

namespace Chess.Users.DataAccess.Entities
{
    public class User : BaseEntity
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