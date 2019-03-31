using System.Collections;
using System.Collections.Generic;
using System.Net;
using Utf8Json;

namespace Modelized.Models
{
    public class ListResponse<T> : Response, IEnumerable<T> where T : IModel
    {
        public IEnumerable<T> Models { get; }

        public ListResponse(IEnumerable<T> models, HttpStatusCode status = HttpStatusCode.OK) : base(status)
        {
            Models = models;
        }

        public override void WriteTo(HttpListenerResponse res)
        {
            base.WriteTo(res);

            using (var stream = res.OutputStream)
            {
                var bytes = JsonSerializer.Serialize(Models);
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Models.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
