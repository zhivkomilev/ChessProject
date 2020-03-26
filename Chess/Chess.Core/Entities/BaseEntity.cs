using System;

namespace Chess.Core.Entities
{
    public abstract class BaseEntity : IBaseEntity
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LatestUpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
