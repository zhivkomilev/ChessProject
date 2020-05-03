using System;

namespace Chess.Users.Models
{
    public interface IBaseModel
    {
        Guid Id { get; set; }
    }
}
