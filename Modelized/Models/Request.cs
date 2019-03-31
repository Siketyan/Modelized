using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using Modelized.Enums;

namespace Modelized.Models
{
    public class Request
    {
        public HttpMethod Method { get; set; }
        public Uri Uri { get; set; }
        public IPAddress Client { get; set; }
        public NameValueCollection Headers { get; }
        public string Payload { get; }

        public Request(HttpListenerRequest req)
        {
            if (Enum.TryParse<HttpMethod>(req.HttpMethod, true, out var method))
            {
                Method = method;
            }

            Uri = req.Url;
            Client = req.RemoteEndPoint?.Address;
            Headers = req.Headers;

            using (var reader = new StreamReader(req.InputStream))
            {
                Payload = reader.ReadToEnd();
            }
        }
    }
}
