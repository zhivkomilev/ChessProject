using Chess.Core.DataAccess;
using Chess.Core.DataAccess.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chess.Users.DataAccess.Infrastructure.ServicesExtensions
{
    public static class ServicesExtensions
    {
        public static void AddDataAccessDependencies(this IServiceCollection services, string connectionString)
        {
            #region Scoped registrations
            services.AddUnitOfWork<UsersDbContext>();
            services.AddActivatorWrapper();
            #endregion

            services.AddDbContext<UsersDbContext>(options 
                => options.UseSqlServer(connectionString));
        }
    }
}