using Chess.Core.Middlewares.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Refit;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chess.Core.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, 
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, $"An error occured while processing the request: {ex.Message}");
                await HandleApiExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occured while processing the request: {ex.Message}");
                await HandleGlobalExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleGlobalExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorModelJson = JsonSerializer.Serialize(new ErrorModel
            {
                StatusCode = context.Response.StatusCode,
                Message = $"Something went wrong while processing your request: {exception.Message}"
            });
            return context.Response.WriteAsync(errorModelJson);
        }

        private Task HandleApiExceptionAsync(HttpContext context, ApiException exception)
        {
            context.Response.ContentType = exception.ContentHeaders.ContentType.MediaType;
            context.Response.StatusCode = (int)exception.StatusCode;

            var errorModelJson = JsonSerializer.Serialize(new ErrorModel
            {
                StatusCode = (int)exception.StatusCode,
                Message = exception.Content
            });

            return context.Response.WriteAsync(errorModelJson);
        }
    }
}