namespace Chess.Users.Models.SettingsModels
{
    public class Smtp
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public bool UseSsl { get; set; }
    }
}