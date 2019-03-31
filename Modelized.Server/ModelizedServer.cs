using System.Net;
using Modelized.Models;

namespace Modelized.Server
{
    public class ModelizedServer
    {
        private readonly Router _router;
        private readonly HttpListener _listener;

        public ModelizedServer(string prefix)
        {
            _router = new Router();
            _listener = new HttpListener();
            _listener.Prefixes.Add(prefix);
        }

        public void Start()
        {
            _listener.Start();
            ListenAsync();
        }

        public void Stop()
        {
            _listener.Stop();
        }

        private async void ListenAsync()
        {
            while (true)
            {
                var context = await _listener.GetContextAsync();
                using (var response = context.Response)
                {
                    _router
                        .Route(new Request(context.Request))
                        .WriteTo(response);
                }
            }
        }
    }
}
