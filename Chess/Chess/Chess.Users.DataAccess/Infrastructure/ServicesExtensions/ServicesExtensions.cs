using Chess.Core.DataAccess;
using Chess.Core.DataAccess.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chess.Users.DataAccess.Infrastructure.ServicesExtensions
{
    public static class ServicesExtensions
    {
        public static void AddDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            #region Scoped registrations
            services.AddScoped<IUnitOfWork, UnitOfWork<UsersDbContext>>();
            services.AddActivatorWrapper();
            #endregion

            services.AddDbContext<UsersDbContext>(options 
                => options.UseSqlServer(configuration.GetConnectionString("UsersDbConnection")));
        }
    }
}