using System;
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
                Console.WriteLine("Hello World");
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
