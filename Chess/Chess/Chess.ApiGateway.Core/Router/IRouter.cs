using Chess.ApiGateway.Core.Router.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace Chess.ApiGateway.Core.Router
{
    public interface IRouter
    {
        Task<HttpResponseMessage> Route(HttpContext context, Route route);
    }
}
