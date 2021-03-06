using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Threading.Tasks;

namespace Calculator.Api
{
    public class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public async static Task Main(string[] args)
        {
            logger.Info("Starting.");
            IHostBuilder hostBuilder = CreateHostBuilder(args);
            var host = hostBuilder.Build();

            try
            {
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, ex.Message);
            }
            finally
            {
                logger.Info("Shuting down.");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
