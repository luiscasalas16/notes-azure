using System;
using System.Diagnostics;

namespace NetFwStorageAccountConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(".Net Framework Console");
                Console.WriteLine($"t: {DateTime.Now.ToString()}");
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
