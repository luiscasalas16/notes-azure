namespace NetWebJobConsole
{
    internal class Program
    {
        //https://github.com/Azure/azure-webjobs-sdk/wiki

        static async Task Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(500);

                Console.WriteLine($"Hello, World {i + 1}!");
            }

            Console.WriteLine($"finish");

            if (new Random().Next(0, 2) == 1)
            {
                Console.WriteLine("STATUS : Failed");

                throw new Exception("error in webjob");
            }
            else
            {
                Console.WriteLine("STATUS : Success");
            }
        }
    }
}