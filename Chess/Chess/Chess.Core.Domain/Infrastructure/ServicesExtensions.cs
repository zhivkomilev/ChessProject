using Chess.Core.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Chess.Core.Domain.Infrastructure
{
    public static class ServicesExtensions
    {
        public static void AddUtilities(this IServiceCollection services)
        {
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        }
    }
}
