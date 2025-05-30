using System;
using System.Diagnostics;

namespace Global
{
    public static class Helpers
    {
        public static string GetDateTime()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time")).ToString("yyyy-MM-dd HH-mm-ss");
        }

        public static void ConsoleWait()
        {
            var current = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Press any key to close this window . . .");
            Console.ForegroundColor = current;
            Console.ReadLine();
        }

        public static void LogSuccess(string message)
        {
            message = GetDateTime() + " " + message;

            Debug.WriteLine(message);

            var current = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = current;
        }

        public static void LogFailure(string message)
        {
            message = GetDateTime() + " " + message;

            Debug.WriteLine(message);

            var current = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = current;
        }

        public static void LogFailure(Exception ex)
        {
            var message = GetDateTime() + " " + ex.ToString();

            Debug.WriteLine(ex.ToString());

            var current = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = current;
        }
    }
}
