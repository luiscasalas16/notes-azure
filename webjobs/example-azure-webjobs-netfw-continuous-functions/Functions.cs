using Microsoft.Azure.WebJobs;

namespace example_azure_webjobs_netfw_continuous_functions
{
    public class Functions
    {
        public static void ProcessQueueMessage([QueueTrigger("%queue%")] string message)
        {
            Program.Write("process message: " + message);
        }
    }
}
