using System;

namespace Common.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void LogInfo(string message)
        {
            ColoredConsoleWrite(ConsoleColor.Cyan, message);
        }

        public void LogError(Exception exception)
        {
            ColoredConsoleWrite(ConsoleColor.DarkRed, string.Format("Exception: {0}\nStack trace: {1}", exception.Message, exception.StackTrace));
        }

        private void ColoredConsoleWrite(ConsoleColor color, string text)
        {
            ColorConsole.WriteInColor(text, color);
        }
    }
}
