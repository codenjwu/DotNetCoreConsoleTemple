using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTemple
{
    public class HostService : IHostedService
    {
        IServiceScopeFactory _scopeFactory;
        readonly IHostApplicationLifetime _appLifetime;
        readonly ILogger<HostService> _logger;
        public HostService(ILogger<HostService> logger,
            IServiceScopeFactory scopeFactory,
            IHostApplicationLifetime appLifetime
            )
        {
            _scopeFactory = scopeFactory;
            _appLifetime = appLifetime;
            _logger = logger;
        }

        private void OnStarted()
        {
            _logger.LogInformation("OnStarted has been called.");
            // Perform post-startup activities here

            using (var scope = _scopeFactory.CreateScope())
            {
                var msService = scope.ServiceProvider.GetRequiredService<IDemoService>();

            }

            // stop application
            _appLifetime.StopApplication();
        }

        private void OnStopping()
        {
            _logger.LogInformation("OnStopping has been called.");

            // Perform on-stopping activities here
        }

        private void OnStopped()
        {
            _logger.LogInformation("OnStopped has been called.");

            // Perform post-stopped activities here
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifetime.ApplicationStarted.Register(OnStarted);
            _appLifetime.ApplicationStopping.Register(OnStopping);
            _appLifetime.ApplicationStopped.Register(OnStopped);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }


    }
}
