using Microsoft.Azure.WebJobs;
using System.Configuration;

namespace example_azure_webjobs_netfw_continuous_functions
{
    public class QueueResolver : INameResolver
    {
        public string Resolve(string name)
        {
            return ConfigurationManager.AppSettings[name].ToString();
        }
    }
}
