using AplicacionMaestro.Application.Features.Articulos.Models;
using AplicacionMaestro.Domain.Entities;
using AutoMapper;

namespace AplicacionMaestro.Infrastructure.Persistence.Mappers
{


    public class ArticuloProfile : Profile
    {
        public ArticuloProfile()
        {
            CreateMap<Articulo, ArticuloLegacySyncModel>()
                .ForMember(dest => dest.IdExternal,
                    opt => opt.MapFrom(src => src.IdExternal))
                .ForMember(dest => dest.Codigo,
                    opt => opt.MapFrom(src => src.Codigo.Valor))
                .ForMember(dest => dest.Descripcion,
                    opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(dest => dest.UnidadMedida,
                    opt => opt.MapFrom(src => src.UnidadMedida))
                .ForMember(dest => dest.Estado,
                    opt => opt.MapFrom(src => src.EstadoMaterial));
        }
    }
}
