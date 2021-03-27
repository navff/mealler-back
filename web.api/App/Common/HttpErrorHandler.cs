using System;
using System.Net;
using System.Threading.Tasks;
using Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace web.api.App.Common
{
    public static class HttpErrorHandler
    {
        public static async Task HandleErrors(HttpContext context)
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature.Error;
            var code = HttpStatusCode.InternalServerError;

            if (exception is EntityNotFoundBaseException) code = HttpStatusCode.NotFound;
            else if (exception is UnauthorizedAccessException) code = HttpStatusCode.Unauthorized;
            else if (exception is ForbiddenAccessException) code = HttpStatusCode.Forbidden;

            context.Response.StatusCode = (int) code;
            var err = new ErrorResponse(exception);
            await context.Response.WriteAsJsonAsync(err);
        }

        private class ErrorResponse
        {
            public ErrorResponse(Exception ex)
            {
                Type = GetTypeName(ex);
                Message = ex.Message;
                StackTrace = ex.ToString();
            }

            private string Type { get; }
            private string Message { get; }
            private string StackTrace { get; }

            private string GetTypeName(Exception exception)
            {
                var type = exception.GetType();
                var result = type.Name;

                if (type.IsGenericType)
                {
                    var g = type.GetGenericTypeDefinition();
                    result = g.Name.Remove(g.Name.IndexOf('`')) + "<" + type.GetGenericArguments()[0].Name + ">";
                }

                return result;
            }
        }
    }
}