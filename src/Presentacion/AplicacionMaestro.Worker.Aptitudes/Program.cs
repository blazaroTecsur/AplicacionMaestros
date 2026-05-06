using AplicacionMaestro.Application.DependencyInjection;
using AplicacionMaestro.Infrastructure.DependencyInjection;
using AplicacionMaestro.Worker.Aptitudes;
using AplicacionMaestro.Worker.Aptitudes.Workers;
using Serilog;

try
{
    Log.Information("Iniciando Worker Aplicación para la Sincronización de Datos Maestros de Aptitudes");

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

    builder.Services.AddHostedService<AptitudSyncWorker>();
    builder.Services.Configure<AptitudSyncWorkerOptions>(
        builder.Configuration.GetSection("WorkerSettings:AptitudSync"));


    var host = builder.Build();
    await host.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "El Worker para Aptitudes falló al iniciar");
}
finally
{
    Log.CloseAndFlush();
}
