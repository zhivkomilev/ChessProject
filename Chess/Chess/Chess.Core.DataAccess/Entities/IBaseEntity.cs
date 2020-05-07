using System;

namespace Chess.Core.DataAccess.Entities
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }

        DateTime CreatedDate { get; set; }

        DateTime LatestUpdateDate { get; set; }
    }
}
