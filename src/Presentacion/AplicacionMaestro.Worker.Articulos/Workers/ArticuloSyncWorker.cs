using AplicacionMaestro.Application;
using AplicacionMaestro.Application.Features.Articulos.Commands;
using AplicacionMaestro.Worker.Articulos.Resilence;
using MediatR;
using Microsoft.Extensions.Options;

namespace AplicacionMaestro.Worker.Articulos.Workers
{
    public class ArticuloSyncWorker : BackgroundService
    {
        private readonly ILogger<ArticuloSyncWorker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ArticuloSyncWorkerOptions _options;

        public ArticuloSyncWorker(
            ILogger<ArticuloSyncWorker> logger,
            IServiceScopeFactory scopeFactory,
            IOptions<ArticuloSyncWorkerOptions> options)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _options = options.Value;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            var retryPolicy = RetryPolicies.CreateDefaultRetryPolicy(_logger);

            _logger.LogInformation(
                "ArticuloSyncWorker iniciado. Intervalo: {Interval} minutos",
            _options.IntervalMinutes);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await retryPolicy.ExecuteAsync(async () =>
                    {
                        using var scope = _scopeFactory.CreateScope();

                        var mediator = scope.ServiceProvider
                            .GetRequiredService<IMediator>();

                        var erpService = scope.ServiceProvider
                            .GetRequiredService<IErpArticuloService>();

                        var ldsService = scope.ServiceProvider
                            .GetRequiredService<ILdsArticuloService>();

                        _logger.LogInformation("Ejecutando sincronización de artículos");

                        var articulosErp = await erpService.ObtenerArticulosAsync(stoppingToken);
                        var articulosLds = await ldsService.ObtenerArticulosAsync(stoppingToken);

                        var articulos = articulosErp
                            .Concat(articulosLds)
                            .ToList();

                        await mediator.Send(
                            new SyncArticulosCommand(articulos),
                            stoppingToken);
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error en ArticuloSyncWorker");
                }

                await Task.Delay(
                    TimeSpan.FromMinutes(_options.IntervalMinutes),
                    stoppingToken);
            }
        }
    }
}
