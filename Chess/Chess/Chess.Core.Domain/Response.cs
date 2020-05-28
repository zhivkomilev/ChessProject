using Chess.Core.Domain.Interfaces;
using System.Net;

namespace Chess.Core.Domain
{
    public class Response : IResponse
    {
        public Response(HttpStatusCode code, bool isSuccessful, object model)
            : this(code, isSuccessful)
        {
            ResponseModel = model;
        }

        public Response(HttpStatusCode code, bool isSuccessful, string message)
            :this(code, isSuccessful)
        {
            Message = message;
        }

        public Response(HttpStatusCode code, bool isSuccessful)
        {
            StatusCode = code;
            IsSuccessful = isSuccessful;
        }

        public HttpStatusCode StatusCode { get; }

        public object ResponseModel { get; set; }

        public string Message { get; set; }

        public bool IsSuccessful { get; }
    }
}