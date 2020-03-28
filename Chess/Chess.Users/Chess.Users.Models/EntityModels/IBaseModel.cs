using System;

namespace Chess.Users.Models.EntityModels
{
    public interface IBaseModel
    {
        Guid Id { get; set; }
    }
}
