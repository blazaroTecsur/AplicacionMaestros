using AplicacionMaestro.Application.Features.Aptitudes.Dtos;
using AplicacionMaestro.Domain.Entities;
using AutoMapper;

namespace AplicacionMaestro.Application.Mapping
{
    public class AptitudProfile : Profile
    {
        public AptitudProfile()
        {
            CreateMap<AptitudSyncDto, Aptitud>()
                .ConstructUsing(dto => new Aptitud(
                    dto.Codigo,
                    dto.Descripcion
                ))
                .ForAllMembers(opts => opts.Ignore());
        }
    }
}
