using System;

namespace Chess.Users.Models.UserModels.Interfaces
{
    public interface IPointsUpdateModel
    {
        Guid UserId { get; set; }
        int Points { get; set; }
    }
}