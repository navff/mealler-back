using System;

namespace web.api.App.Common
{
    public class EntityCreatedResult
    {
        public EntityCreatedResult(int id)
        {
            Id = id;
        }

        public EntityCreatedResult(Exception error)
        {
            Error = error;
        }

        public int Id { get; }
        public Exception Error { get; }
    }
}