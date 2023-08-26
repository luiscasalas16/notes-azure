using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace NetFwVirtualMachineWebMvc
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var defaultSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            JsonConvert.DefaultSettings = () => { return defaultSettings; };

            config.Formatters.JsonFormatter.SerializerSettings = defaultSettings;

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
