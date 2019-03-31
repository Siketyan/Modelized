using System.Net;
using Utf8Json;

namespace Modelized.Models
{
    public class Response
    {
        public HttpStatusCode Status { get; }

        public Response(HttpStatusCode status = HttpStatusCode.NoContent)
        {
            Status = status;
        }

        public virtual void WriteTo(HttpListenerResponse res)
        {
            res.StatusCode = (int) Status;
        }
    }

    public class Response<T> : Response where T : IModel
    {
        public T Model { get; }

        public Response(T model, HttpStatusCode status = HttpStatusCode.OK) : base(status)
        {
            Model = model;
        }

        public override void WriteTo(HttpListenerResponse res)
        {
            base.WriteTo(res);

            using (var stream = res.OutputStream)
            {
                var bytes = JsonSerializer.Serialize(Model);
                stream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
