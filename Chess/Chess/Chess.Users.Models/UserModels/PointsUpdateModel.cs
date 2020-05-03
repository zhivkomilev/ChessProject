using Chess.Users.Models.UserModels.Interfaces;
using System;

namespace Chess.Users.Models.UserModels
{
    public class PointsUpdateModel : IPointsUpdateModel
    {
        public Guid UserId { get; set; }
        public int Points { get; set; }
    }
}