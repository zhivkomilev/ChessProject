using Chess.ApiGateway.Core.Router.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Chess.ApiGateway.Core.Router
{
    public class Router : IRouter
    {
        private readonly HttpClient _httpClient;
        public Router(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<HttpResponseMessage> Route(HttpContext context, Route route)
        {
            if (context.Request.Path != route.EndpointPath)
            {
                return await Task.FromResult(new HttpResponseMessage(HttpStatusCode.BadRequest));
            }

            var destinationUri = new UriBuilder
            {
                Scheme = route.Destination.DestinationScheme,
                Host = route.Destination.Host,
                Port = route.Destination.Port,
                Path = route.Destination.EndpointPath
            };

            return null;
        }
    }
}
