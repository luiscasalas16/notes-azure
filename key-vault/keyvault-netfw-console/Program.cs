using System;
using System.Configuration;

namespace keyvault_netfw_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"SecretNameKeyVault {ConfigurationManager.AppSettings["SecretNameKeyVault"]}");
            Console.WriteLine($"SecretNameUserSecrets {ConfigurationManager.AppSettings["SecretNameUserSecrets"]}");
            Console.WriteLine($"SecretNameAppSettings {ConfigurationManager.AppSettings["SecretNameAppSettings"]}");
            Console.ReadLine();
        }
    }
}
