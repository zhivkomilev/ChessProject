using System;

namespace Chess.Core.Domain.Interfaces
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }

        DateTime Now { get; }
    }
}
