using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Users.Models.SettingsModels
{
    public class EmailSettings
    {
        public Smtp Smtp { get; set; }
        public EmailUrls EmailUrls { get; set; }
    }
}