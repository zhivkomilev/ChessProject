using System.Collections.Generic;

namespace Chess.Users.Models.SettingsModels
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Key { get; set; }
        public string[] Audiences { get; set; }
        public int ExpirationInMinutes { get; set; }
    }
}
