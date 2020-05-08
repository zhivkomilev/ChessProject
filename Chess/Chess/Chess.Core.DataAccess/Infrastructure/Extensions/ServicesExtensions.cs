using Chess.Core.DataAccess.Wrappers;
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
    }
}
