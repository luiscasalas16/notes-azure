using System;
using System.Globalization;
using System.Threading;

namespace example_azure_webjobs_netfw_continuous
{
    internal class Program
    {
        static void Main()
        {
            foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones())
            {
                // For a Console App
                Console.WriteLine(z.Id + "," + z.BaseUtcOffset + "," + z.StandardName + "," + z.DisplayName + "," + z.DaylightName);
                // For any other App
                System.Diagnostics.Debug.WriteLine(z.Id + "," + z.BaseUtcOffset + "," + z.StandardName + "," + z.DisplayName + "," + z.DaylightName);
            }

            for (int i = 0; i < int.MaxValue; i++)
            {
                Thread.Sleep(1000);

                Write($"{i}");
            }
        }

        public static void Write(string message)
        {
            Console.WriteLine("example-azure-webjobs-netfw-continuous" + " " + TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Central America Standard Time") + " " + message);
        }
    }
}
