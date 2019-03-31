using System;

namespace Modelized.Server.Loggers
{
    public class ConsoleLogger : ILogger
    {
        private const string PrefixDebug = "Debug";
        private const string PrefixInfo = "Info";
        private const string PrefixWarn = "Warn";
        private const string PrefixError = "Error";
        private const string Format = "{0}/{1}: {2}";

        public void Debug(string message)
        {
            Log(PrefixDebug, message);
        }

        public void Info(string message)
        {
            Log(PrefixInfo, message);
        }

        public void Warn(string message)
        {
            Log(PrefixWarn, message);
        }

        public void Error(string message)
        {
            Log(PrefixError, message);
        }

        public void Error(Exception e)
        {
            Error(
                $"An exception occured on {e.Source}:\n{e.Message}\n{e.StackTrace}"
            );
        }

        private static void Log(string prefix, string message)
        {
            var time = DateTime.Now.ToLongTimeString();
            Console.WriteLine(Format, time, prefix, message);
        }
    }
}
