using System;
using System.Configuration;
using System.Diagnostics;

namespace NetFwKeyVaultConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(".Net Framework Console");
                Console.WriteLine($"SecretNameKeyVault: {ConfigurationManager.AppSettings["SecretNameKeyVault"]}");
                Console.WriteLine($"SecretNameUserSecrets: {ConfigurationManager.AppSettings["SecretNameUserSecrets"]}");
                Console.WriteLine($"SecretNameAppSettings: {ConfigurationManager.AppSettings["SecretNameAppSettings"]}");
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());

                Console.WriteLine(ex.ToString());
            }

            Console.Write("Press enter to close this window . . .");
            Console.ReadLine();
        }
    }
}
