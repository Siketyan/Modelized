using System;

namespace Modelized.Models
{
    public class ErrorModel : IModel
    {
        public string Error { get; }

        public ErrorModel(Exception e)
        {
            Error = e.Message;
        }
    }
}
