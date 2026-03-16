using AplicacionMaestro.Application.DependencyInjection;
using AplicacionMaestro.Infrastructure.DependencyInjection;
using AplicacionMaestro.Worker.Almacenes;
using AplicacionMaestro.Worker.Almacenes.Workers;
using Serilog;

try
{
    Log.Information("Iniciando Worker Aplicación para la Sincronización de Datos Maestros de Almacenes");

    var builder = Host.CreateApplicationBuilder(args);

    // ?? leer serilog desde appsettings

    // Configurar Serilog desde appsettings
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

    // ? SE REGISTRA ASÍ

    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddHostedService<AlmacenSyncWorker>();
    builder.Services.Configure<AlmacenSyncWorkerOptions>(
        builder.Configuration.GetSection("WorkerSettings:AlmacenSync"));


    var host = builder.Build();
    await host.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "El Worker para Almacenes falló al iniciar");
}
finally
{
    Log.CloseAndFlush();
}
