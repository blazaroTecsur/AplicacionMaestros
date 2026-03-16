using AplicacionMaestro.Application.Features.CodsUnidad1.Dtos;
using AplicacionMaestro.Domain.Entities;
using AutoMapper;

namespace AplicacionMaestro.Application.Mapping
{
    public class CodigoUnidad1Profile : Profile
    {
        public CodigoUnidad1Profile()
        {
            CreateMap<CodigoUnidad1SyncDto, CodUnidad1>()
                .ConstructUsing(dto => new CodUnidad1(
                    dto.Codigo,
                    dto.Descripcion
                ))
                .ForAllMembers(opts => opts.Ignore());
        }
    }
}
