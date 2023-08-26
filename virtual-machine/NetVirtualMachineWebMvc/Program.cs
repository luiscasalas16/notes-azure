using System.Diagnostics;

namespace NetVirtualMachineWebMvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

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