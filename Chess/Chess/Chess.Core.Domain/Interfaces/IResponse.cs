using System.Net;

namespace Chess.Core.Domain.Interfaces
{
    public interface IResponse
    {
        public HttpStatusCode StatusCode { get; }
        public object ResponseModel { get; set; }
        public string Message { get; set; }
        public bool IsSuccessful { get; }
    }
}
