using Microsoft.AspNetCore.Builder;

namespace Chess.Core.Middlewares.BuilderExtensions
{
    public static class AppBuilderExtensions
    {
        public static void AddGlobalExceptionHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
