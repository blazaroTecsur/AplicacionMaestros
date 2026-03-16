using AplicacionMaestro.Application.Features.Almacenes.Handlers;
using AplicacionMaestro.Application.Features.Aptitudes.Handlers;
using AplicacionMaestro.Application.Features.Certificados.Handlers;
using AplicacionMaestro.Application.Features.CodsUnidad1.Handlers;
using AplicacionMaestro.Application.Features.CtasContables.Handlers;
using AplicacionMaestro.Application.Features.Empleados.Handlers;
using AplicacionMaestro.Application.Features.Socios.Handlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AplicacionMaestro.Application.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);

            services.AddAutoMapper(typeof(SyncArticulosHandler).Assembly);
            services.AddAutoMapper(typeof(SyncProveedoresHandler).Assembly);
            services.AddAutoMapper(typeof(SyncSociosHandler).Assembly);
            services.AddAutoMapper(typeof(SyncAptitudesHandler).Assembly);
            services.AddAutoMapper(typeof(SyncCertificadosHandler).Assembly);
            services.AddAutoMapper(typeof(SyncAlmacenesHandler).Assembly);
            services.AddAutoMapper(typeof(SyncEmpleadosHandler).Assembly);
            services.AddAutoMapper(typeof(SyncCodsUnidad1Handler).Assembly);
            services.AddAutoMapper(typeof(SyncCtasContablesHandler).Assembly);

            return services;
        }
    }
}
