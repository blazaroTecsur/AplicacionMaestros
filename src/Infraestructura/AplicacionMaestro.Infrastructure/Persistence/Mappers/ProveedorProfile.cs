using AplicacionMaestro.Application;
using AplicacionMaestro.Application.Features.Proveedores.Models;
using AplicacionMaestro.Domain.Entities;
using AutoMapper;


namespace AplicacionMaestro.Infrastructure.Persistence.Mappers
{
    public class ProveedorProfile : Profile
    {
        public ProveedorProfile()
        {
            CreateMap<Proveedor, ProveedorLegacySyncModel>()
                .ForMember(dest => dest.IdExternal, opt => opt.MapFrom(src => src.IdExternal))
                .ForMember(dest => dest.Ruc, opt => opt.MapFrom(src => src.Ruc))
                .ForMember(dest => dest.RazonSocial, opt => opt.MapFrom(src => src.RazonSocial))
                .ForMember(dest => dest.ContactoNombre, opt => opt.MapFrom(src => src.Contacto.Nombre))
                .ForMember(dest => dest.ContactoTelefono, opt => opt.MapFrom(src => src.Contacto.Telefono))
                .ForMember(dest => dest.CorreoExt, opt => opt.MapFrom(src => src.Contacto.CorreoExterno))
                .ForMember(dest => dest.CorreoInt, opt => opt.MapFrom(src => src.Contacto.CorreoInterno));
        }
    }
}   
