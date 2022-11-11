using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConfigTestConsoleApp
{
    public class SampleHostingService : IHostedService
    {
        private readonly IDisposable _configChangeHandle;
        private IOptions<FunctionsHostingConfig> _functionsHostingConfig;
        private IOptionsMonitor<FunctionsHostingConfig> _monitor;
        private System.Timers.Timer _timer;

        public SampleHostingService(IOptions<FunctionsHostingConfig> functionsHostingConfig, IOptionsMonitor<FunctionsHostingConfig> monitor)
        {
            _configChangeHandle = monitor.OnChange(UpdateConfiguration);
            _functionsHostingConfig = functionsHostingConfig;
            _monitor = monitor;

            _timer = new System.Timers.Timer()
            {
                AutoReset = false,
                Interval = 1000
            };
            _timer.Elapsed += OnTimer;
            _timer.Start();
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
        private void OnTimer(object sender, System.Timers.ElapsedEventArgs e)
        {
            bool enabled1 = _functionsHostingConfig.Value.SomeFeatureEnabled;
            bool enabled2 = _monitor.CurrentValue.SomeFeatureEnabled;
            _timer.Start();
        }
    }
}
