using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.ApiGateway.Core.AppBuilderExtensions
{
    public static class Extensions
    {
        public static void AddChessApiGatewayDependencies(this IServiceCollection services)
        {
            services.AddHttpClient();

        }

        public static void UseChessApiGateway(this IApplicationBuilder appBuider)
        {

        }
    }
}
