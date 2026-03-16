using AplicacionMaestro.Application.DependencyInjection;
using AplicacionMaestro.Infrastructure.DependencyInjection;
using AplicacionMaestro.Worker.Articulos.Workers;
using AplicacionMaestro.Worker.Local;
using AplicacionMaestro.Worker.Proveedores.Workers;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging.AddSerilog();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// 👇 levantamos ambos
builder.Services.AddHostedService<ArticuloSyncWorker>();
builder.Services.AddHostedService<ProveedorSyncWorker>();
builder.Services.Configure<LocalSyncWorkerOptions>(
    builder.Configuration.GetSection("WorkerSettings:LocalSync"));

var host = builder.Build();
host.Run();