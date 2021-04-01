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

    public abstract class EntityNotFoundBaseException : Exception
    {
        protected EntityNotFoundBaseException(string message = null) : base(message)
        {
        }

        protected EntityNotFoundBaseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}