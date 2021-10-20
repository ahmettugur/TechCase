using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using TechCase.Api.Extensions;
using TechCase.Infrastructure.Database.SqlServer;

namespace TechCase.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(GetConfiguration(), args);
            host.MigrateDbContext<TechCaseContext>((context, services) =>
            {
                var logger = services.GetService<ILogger<TechCaseContext>>();

                var dbContextSeeder = new TechCaseContextSeed();
                dbContextSeeder.SeedAsync(context, logger)
                    .Wait();
            });

            host.Run();
        }
        static IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
                WebHost.CreateDefaultBuilder(args)
                    .UseDefaultServiceProvider((context, options) =>
                    {
                        options.ValidateOnBuild = false;
                    })
                    .ConfigureAppConfiguration(i => i.AddConfiguration(configuration))
                .UseStartup<Startup>()
                .Build();


        static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
