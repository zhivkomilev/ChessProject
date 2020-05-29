using Chess.Core.DataAccess.Entities;
using System.Collections.Generic;

namespace Chess.GameEngine.DataAccess.Entities
{
    public class Player : BaseEntity
    {
        public string Username { get; set; }
        public int Points { get; set; }
    }
}