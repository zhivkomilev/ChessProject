using Chess.Users.DataAccess.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chess.Users.DataAccess.Entities
{
    public class ChangePassword : BaseEntity
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string Token { get; set; }
        public Status Status { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}