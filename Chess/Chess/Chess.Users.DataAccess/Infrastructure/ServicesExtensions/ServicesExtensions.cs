using Chess.Users.DataAccess.Repositories;
using Chess.Users.DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Chess.Users.DataAccess.Infrastructure.ServicesExtensions
{
    public static class ServicesExtensions
    {
        public static void AddUnitOfWork(this IServiceCollection services)
        {
            #region Scoped registrations
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion
        }
    }
}
