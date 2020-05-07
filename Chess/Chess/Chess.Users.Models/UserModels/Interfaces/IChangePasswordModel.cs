using System;

namespace Chess.Users.Models.UserModels.Interfaces
{
    public interface IChangePasswordModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
