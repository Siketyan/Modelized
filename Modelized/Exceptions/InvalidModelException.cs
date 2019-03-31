using System;

namespace Modelized.Exceptions
{
    public class InvalidModelException : Exception
    {
        public InvalidModelException(string message) : base(message)
        {
        }
    }
}
