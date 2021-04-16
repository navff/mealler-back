using System;

namespace Common.Exceptions
{
    public class NoAuthenticationException : Exception
    {
        public NoAuthenticationException(string? message) : base(message)
        {
        }
    }
}