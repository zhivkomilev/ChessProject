using System;

namespace Chess.Users.DataAccess.Entities
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }

        DateTime CreatedDate { get; set; }

        DateTime LatestUpdateDate { get; set; }

        bool IsDeleted { get; set; }
    }
}
