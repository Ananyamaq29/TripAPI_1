using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
 
namespace ProductApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
 
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices(services =>
                    {
                        // Add controllers as a service
                        services.AddControllers();
                    });
 
                    webBuilder.Configure(app =>
                    {
                        // Configure middleware pipeline
                        app.UseHttpsRedirection(); // Redirect HTTP to HTTPS
                        app.UseRouting();          // Set up routing
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();  // Map controller actions to routes
                        });
                    });
                });
    }
}
 