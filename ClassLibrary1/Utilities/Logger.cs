using System;

namespace SeleniumPom_v1.Utilities
{
    public static class Logger
    {
        public static void LogInfo(string message)
        {
            Log(message, "INFO");
        }

        public static void LogError(string message)
        {
            Log(message, "ERROR");
        }

        public static void LogWarning(string message)
        {
            Log(message, "WARN");
        }

        private static void Log(string message, string level)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [{level}] - {message}");
        }
    }
}
