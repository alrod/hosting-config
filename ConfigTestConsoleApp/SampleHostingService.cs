using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigTestConsoleApp
{
    public class SampleHostingService : IHostedService
    {
        private readonly IDisposable _configChangeHandle;
        private IOptions<FunctionsHostingConfig> _functionsHostingConfig;

        public SampleHostingService(IOptions<FunctionsHostingConfig> functionsHostingConfig, IOptionsMonitor<FunctionsHostingConfig> monitor)
        {
            _configChangeHandle = monitor.OnChange(UpdateConfiguration);
            _functionsHostingConfig = functionsHostingConfig;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            bool enabled = _functionsHostingConfig.Value.SomeFeatureEnabled;
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _configChangeHandle.Dispose();
            return Task.CompletedTask;
        }

        private void UpdateConfiguration(FunctionsHostingConfig config)
        {
            // Read updated config
            bool enabled = _functionsHostingConfig.Value.SomeFeatureEnabled;
        }
    }
}
