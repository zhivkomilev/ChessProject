using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Users.Models.EntityModels.UserModels.Interfaces
{
    public interface IUserModel : IBaseModel
    {
        string Username { get; set; }

        string Password { get; set; }

        string Email { get; set; }

        int Points { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }
    }
}
