using System;
using System.Net;

namespace Modelized.Exceptions
{
    public class ModelizedException : Exception
    {
        private const string InternalExceptionMessage = "An internal exception occured.";

        public HttpStatusCode Status { get; }

        public ModelizedException(HttpStatusCode status, string message) : base(message)
        {
            Status = status;
        }

        public ModelizedException(Exception e) : base(InternalExceptionMessage, e)
        {
            Status = HttpStatusCode.InternalServerError;
        }
    }
}
