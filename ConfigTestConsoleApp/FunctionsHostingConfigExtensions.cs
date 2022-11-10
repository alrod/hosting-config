using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IO;

namespace ConfigTestConsoleApp
{
    public static class FunctionsHostingConfigExtensions
    {
        public static IConfigurationBuilder AddFunctionsHostingConfig
            (this IConfigurationBuilder builder)
        {
            return builder.Add(new FunctionsHostingConfigSource());
        }
    }
}
