namespace Chess.Users.Models.UserModels.Interfaces
{
    public interface IUserDetailsModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public int Points { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
