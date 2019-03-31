using System;
using Modelized.Server.Loggers;

namespace Modelized.Server.Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var server = new ModelizedServer(
                "http://localhost:32321/"
            );

            server.Start();
            Log.Info("Server started");
            Console.ReadLine();
            server.Stop();
        }
    }
}
