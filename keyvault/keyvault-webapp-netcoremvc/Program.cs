using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Configuration;

namespace keyvault_webapp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            /*
            "AZURE_TENANT_ID": "c3db3a9b-847b-4bbe-b592-62ad5d4f6918",
            "AZURE_CLIENT_ID": "c13ff45e-b0aa-495e-865d-87714cea7d39",
            "AZURE_CLIENT_SECRET": "3ef8Q~2ILBT-Hn~6h-L3c01XwM85cDE~6c9-Pbyc"
            */

            /*
            //service principal in multiple locations

            TokenCredential credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions()
            {
                ExcludeVisualStudioCredential = true,
                ExcludeVisualStudioCodeCredential = true,
                ExcludeAzureCliCredential = true,
                ExcludeAzurePowerShellCredential = true
            });
            */

            //service principal in appsettings.json

            TokenCredential credential = new ClientSecretCredential
            (
                builder.Configuration.GetValue<string>("AZURE_TENANT_ID"),
                builder.Configuration.GetValue<string>("AZURE_CLIENT_ID"),
                builder.Configuration.GetValue<string>("AZURE_CLIENT_SECRET")
            );

            builder.Configuration.AddAzureKeyVault(new Uri("https://luiscasalas16-key-vault.vault.azure.net/"), credential);            

            var app = builder.Build();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}