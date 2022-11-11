using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConfigTestConsoleApp
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args);
            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddFunctionsHostingConfig();
            })
            .ConfigureServices((context, services) =>
            {
                var separateConfig = new ConfigurationBuilder()
                    .AddFunctionsHostingConfig()
                    .Build();

                services.AddHostedService<SampleHostingService>();

                services.ConfigureOptions<FunctionsHostingConfigOptionsSetup>();
                services.Configure<FunctionsHostingConfig>(separateConfig);
            });

            return builder;
        }
    }
}

