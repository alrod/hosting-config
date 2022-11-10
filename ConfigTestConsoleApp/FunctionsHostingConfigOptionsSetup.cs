using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTestConsoleApp
{
    internal class FunctionsHostingConfigOptionsSetup : IConfigureOptions<FunctionsHostingConfig>
    {
        private readonly IConfiguration _configuration;

        public FunctionsHostingConfigOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(FunctionsHostingConfig options)
        {
            ConfigurationRoot configRoot = _configuration as ConfigurationRoot;
            if (configRoot != null)
            {
                IConfigurationProvider? provider = configRoot.Providers.SingleOrDefault(x => x.GetType() == typeof(FunctionsHostingConfigProvider));
                if (provider != null)
                {
                    var keys = provider.GetChildKeys(new string[] { }, null);
                    foreach (string key in keys)
                    {
                        if (provider.TryGet(key, out string? value))
                        {
                            if (!string.IsNullOrEmpty(value))
                            {
                                options[key] = value;
                            }
                        }

                    }
                }
            }
        }
    }
}
