using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;

                    config.AddJsonFile("appsettings.json", false, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, reloadOnChange: true);

                    if (env.IsDevelopment())
                    {
                        config.AddUserSecrets("5af4a307-d2b5-448f-8084-f5abbc1cb271", reloadOnChange: true);
                    }

                    config.AddEnvironmentVariables(prefix: "ISO_");
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
