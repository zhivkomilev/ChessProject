using Chess.Users.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Chess.Users.Services.Infrastructure.Services
{
    public static class ServicesExtension
    {
        public static void AddUserServices(this IServiceCollection services)
        {
            #region Scoped registrations
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            #endregion
        }
    }
}
