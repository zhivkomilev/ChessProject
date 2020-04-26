namespace Chess.Users.Models.EmailModels
{
    public class ChangePasswordEmailModel : EmailModel
    {
        public string Token { get; set; }

        public string ChangePasswordUrl { get; set; }
    }
}