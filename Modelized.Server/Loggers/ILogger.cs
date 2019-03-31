using System;

namespace Modelized.Server.Loggers
{
    public interface ILogger
    {
        void Debug(string message);
        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Error(Exception e);
    }
}
