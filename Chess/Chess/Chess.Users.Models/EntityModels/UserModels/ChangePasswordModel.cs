using System;

namespace Chess.Users.Models.EntityModels.UserModels
{
    public class ChangePasswordModel
    {
        public Guid UserId{ get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}