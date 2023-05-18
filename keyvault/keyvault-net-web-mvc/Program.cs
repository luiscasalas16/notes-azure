using Azure.Core;
using Azure.Identity;
using System.Diagnostics;

namespace keyvault_net_web_mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                TokenCredential credential = new DefaultAzureCredential();

                builder.Configuration.AddAzureKeyVault(new Uri("https://luiscasalas16-key-vault.vault.azure.net/"), credential);

                builder.Services.AddControllersWithViews();

                var app = builder.Build();

                app.UseStaticFiles();

                app.UseRouting();

                app.UseAuthorization();

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                app.Run();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());

                Console.WriteLine(ex.ToString());
            }
        }
    }
}