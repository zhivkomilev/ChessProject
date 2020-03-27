using System;

namespace Chess.Users.DataAccess.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LatestUpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
