using System;

namespace Common.Exceptions
{
    public class ForbiddenAccessBaseException : Exception
    {
        public ForbiddenAccessBaseException(string? message) : base(message)
        {
        }
    }

    public class ForbiddenAccessException<T> : ForbiddenAccessBaseException
    {
        public ForbiddenAccessException(int id, string message = null)
            : base($"You not allowed to {typeof(T).Name} with Id={id}. {message}")
        {
        }
    }
}