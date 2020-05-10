using Chess.Core.Domain.Interfaces;
using System;

namespace Chess.Core.Domain
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;

        public DateTime Now => DateTime.Now;
    }
}
