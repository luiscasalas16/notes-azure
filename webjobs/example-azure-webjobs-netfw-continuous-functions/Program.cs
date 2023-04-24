using Microsoft.Azure.WebJobs;
using System;
using System.Configuration;
using System.Globalization;

namespace example_azure_webjobs_netfw_continuous_functions
{
    internal class Program
    {
        static void Main()
        {
            Write("init");

            Write("queue " + ConfigurationManager.AppSettings["queue"]);

            var config = new JobHostConfiguration()
            {
                DashboardConnectionString = ConfigurationManager.ConnectionStrings["AzureWebJobsDashboard"].ConnectionString,
                StorageConnectionString = ConfigurationManager.ConnectionStrings["AzureWebJobsDashboard"].ConnectionString,
                NameResolver = new QueueResolver()
            };

            if (config.IsDevelopment)
            {
                config.UseDevelopmentSettings();
            }

            var host = new JobHost(config);

            Write("run");

            host.RunAndBlock();
        }

        public static void Write(string message)
        {
            Console.WriteLine("example-azure-webjobs-netfw-continuous-functions" + " " + TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Central America Standard Time") + " " + message);
        }
    }
}
