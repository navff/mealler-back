using System;

namespace Common.Exceptions
{
    public class EntityNotFoundException<T> : EntityNotFoundBaseException
    {
        public EntityNotFoundException(int id, string message = "")
            : base($"{typeof(T).Name} with Id={id} was not found. {message}")
        {
        }

        public EntityNotFoundException(string id, string message = "")
            : base($"{typeof(T).Name} with Id={id} was not found. {message}")
        {
        }
    }

    public class EntityNotFoundBaseException : Exception
    {
        public EntityNotFoundBaseException(string? message) : base(message)
        {
        }

        public EntityNotFoundBaseException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}