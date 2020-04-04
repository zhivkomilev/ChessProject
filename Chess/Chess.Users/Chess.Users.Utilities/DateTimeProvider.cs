using Chess.Users.Utilities.Interfaces;
using System;

namespace Chess.Users.Utilities
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;

        public DateTime Now => DateTime.Now;
    }
}
