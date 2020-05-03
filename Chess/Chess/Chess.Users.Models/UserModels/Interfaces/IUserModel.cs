namespace Chess.Users.Models.UserModels.Interfaces
{
    public interface IUserModel : IBaseModel
    {
        string Username { get; set; }

        string Password { get; set; }

        string Email { get; set; }

        int Points { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }
    }
}
