namespace Chess.Users.Models.EntityModels.UserModels.Interfaces
{
    public interface IUserModel : IBaseModel
    {
        string Username { get; set; }
        string Password { get; set; }
        string Email { get; set; }
    }
}
