using System.Net;
using Modelized.Models;

namespace Modelized.Exceptions
{
    public class NotFoundException : ModelizedException
    {
        public NotFoundException(string message) : base(HttpStatusCode.NotFound, message)
        {
        }
    }

    public class NotFoundException<T> : NotFoundException where T : IModel
    {
        public NotFoundException() : base($"No entity of the model {typeof(T).Name} found.")
        {
        }
    }
}
