using System;

namespace Common
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
            : base("Entity not found")
        { }
    }
}
