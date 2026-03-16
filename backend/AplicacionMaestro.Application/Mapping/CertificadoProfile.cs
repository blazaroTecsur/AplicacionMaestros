using AplicacionMaestro.Application.Features.Certificados.Dtos;
using AplicacionMaestro.Domain.Entities;
using AutoMapper;

namespace AplicacionMaestro.Application.Mapping
{
    public class CertificadoProfile : Profile
    {
        public CertificadoProfile()
        {
            CreateMap<CertificadoSyncDto, Certificado>()
                .ConstructUsing(dto => new Certificado(
                    dto.Codigo,
                    dto.Descripcion
                ))
                .ForAllMembers(opts => opts.Ignore());
        }
    }
}
