using System;

namespace UnityEngine.Exceptions
{
    public class DublicateComponentException : ApplicationException
    {
        public DublicateComponentException()
            : base("Unable to create two instance of unique component")
        { }

        public DublicateComponentException(String msg)
            : base(msg)
        { }
    }
}
