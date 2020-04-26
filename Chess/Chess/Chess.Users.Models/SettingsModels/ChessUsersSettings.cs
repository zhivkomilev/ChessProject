namespace Chess.Users.Models.SettingsModels
{ 
    public class ChessUsersSettings
    {
        public JwtSettings JwtSettings { get; set; }
        public EmailSettings EmailSettings { get; set; }
    }
}
