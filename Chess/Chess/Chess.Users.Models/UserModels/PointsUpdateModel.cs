using Chess.Users.Models.UserModels.Interfaces;
using System;

namespace Chess.Users.Models.UserModels
{
    public class PointsUpdateModel : IPointsUpdateModel
    {
        public int Points { get; set; }
    }
}