using System;

namespace Chess.ApiGateway.Api.ApiServices.UsersService.Models
{
    public class ChangePasswordModel
    {

        public Guid UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
