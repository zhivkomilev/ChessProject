using Chess.Users.Models.UserModels.Interfaces;
using System;

namespace Chess.Users.Models.UserModels
{
    public class ChangePasswordModel : IChangePasswordModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}