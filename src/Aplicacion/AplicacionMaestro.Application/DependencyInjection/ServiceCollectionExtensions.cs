using AplicacionMaestro.Application.Behaviors;
using AplicacionMaestro.Application.Features.Almacenes.Handlers;
using AplicacionMaestro.Application.Features.Aptitudes.Handlers;
using AplicacionMaestro.Application.Features.Certificados.Handlers;
using AplicacionMaestro.Application.Features.CodsUnidad1.Handlers;
using AplicacionMaestro.Application.Features.CtasContables.Handlers;
using AplicacionMaestro.Application.Features.Empleados.Handlers;
using AplicacionMaestro.Application.Features.Socios.Handlers;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AplicacionMaestro.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        var assembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddMediatR(assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(assembly);

        services.AddAutoMapper(
            typeof(SyncArticulosHandler).Assembly,
            typeof(SyncProveedoresHandler).Assembly,
            typeof(SyncSociosHandler).Assembly,
            typeof(SyncAptitudesHandler).Assembly,
            typeof(SyncCertificadosHandler).Assembly,
            typeof(SyncAlmacenesHandler).Assembly,
            typeof(SyncEmpleadosHandler).Assembly,
            typeof(SyncCodsUnidad1Handler).Assembly,
            typeof(SyncCtasContablesHandler).Assembly);

        return services;
    }
}
