using System;

namespace Common.Logging
{
    public interface ILogger
    {
        void LogInfo(string message);

        void LogError(Exception exception);
    }
}
