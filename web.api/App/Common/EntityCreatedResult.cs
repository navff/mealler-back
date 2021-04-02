using System;

namespace web.api.App.Common
{
    public class EntityCreatedResult
    {
        public EntityCreatedResult(int id)
        {
            Id = id;
        }

        public EntityCreatedResult(Exception exception)
        {
            Error = new ErrorResult(
                exception.GetType().ToString(),
                exception.Message,
                exception.InnerException?.Message,
                exception.StackTrace);
        }

        public int Id { get; }
        public ErrorResult Error { get; }
    }

    public class ErrorResult
    {
        public ErrorResult(
            string type,
            string message,
            string innerMessage,
            string stackTrace)
        {
            Type = type;
            Message = message;
            InnerMessage = innerMessage;
            StackTrace = stackTrace;
        }

        string Type { get; }
        string Message { get; }
        string InnerMessage { get; }
        string StackTrace { get; }
    }
}