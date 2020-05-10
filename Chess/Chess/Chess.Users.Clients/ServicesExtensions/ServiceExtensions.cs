using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace Chess.Users.Clients.ServicesExtensions
{
    public static class ServiceExtensions
    {
        public static void AddUserServiceClients(this IServiceCollection services, string userServiceBaseUrl, string authServiceBaseUrl)
        {
            services.AddRefitClient<IAuthService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(authServiceBaseUrl));
            services.AddRefitClient<IUsersService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(userServiceBaseUrl));
        }
    }
}
