using Chess.Users.Utilities.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Chess.Users.Utilities.Infrastructure
{
    public static class ServicesExtensions
    {
        public static void AddUtilities(this IServiceCollection services)
        {
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        }
    }
}
