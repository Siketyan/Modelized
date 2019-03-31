using System;

namespace Modelized.Server.Loggers
{
    public static class Log
    {
        private static readonly ILogger Logger = new ConsoleLogger();

        public static void Debug(string message)
        {
            Logger.Debug(message);
        }

        public static void Info(string message)
        {
            Logger.Info(message);
        }

        public static void Warn(string message)
        {
            Logger.Warn(message);
        }

        public static void Error(string message)
        {
            Logger.Error(message);
        }

        public static void Error(Exception e)
        {
            Logger.Error(e);
        }
    }
}
