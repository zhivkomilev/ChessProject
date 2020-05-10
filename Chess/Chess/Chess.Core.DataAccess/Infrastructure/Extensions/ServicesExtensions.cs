using Chess.Core.DataAccess.Wrappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chess.Core.DataAccess.Infrastructure.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddActivatorWrapper(this IServiceCollection services)
        {
            #region Scoped registrations
            services.AddScoped<IActivatorWrapper, ActivatorWrapper>();
            #endregion
        }

        public static void AddUnitOfWork<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<TDbContext>>();
        }
    }
}
