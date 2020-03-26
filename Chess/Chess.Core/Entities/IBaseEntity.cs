using System;

namespace Chess.Core.Entities
{
    public interface IBaseEntity
    {
        string Id { get; set; }

        DateTime CreatedDate { get; set; }

        DateTime LatestUpdateDate { get; set; }

        bool IsDeleted { get; set; }
    }
}
