using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ConsoleTemple
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args);
            await host.RunConsoleAsync();
        }
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                 .ConfigureAppConfiguration((context, config) =>
                 {
                     config.SetBasePath(AppContext.BaseDirectory)
                     .AddJsonFile("appsettings.json", true, true)
                     .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true, true)
                     .AddEnvironmentVariables();
                 })
                 .ConfigureLogging((context, loger) =>
                 {
                     loger.AddConfiguration(context.Configuration);
                     loger.SetMinimumLevel(LogLevel.Information);
                     loger.AddConsole();
                 })
                 .ConfigureServices((context, services) => new Startup(context.Configuration).ConfigureServices(services));
        }
    }
}
