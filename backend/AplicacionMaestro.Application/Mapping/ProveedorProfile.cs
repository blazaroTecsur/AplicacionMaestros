using AplicacionMaestro.Application.Common.Mappings;
using AplicacionMaestro.Domain.Entities;
using AplicacionMaestro.Domain.ValueObject;
using AutoMapper;

namespace AplicacionMaestro.Application.Mapping;

public class ProveedorProfile : Profile
{
    public ProveedorProfile()
    {
        CreateMap<ProveedorSyncDto, Proveedor>()
            .ConstructUsing(dto =>
                Proveedor.Create(
                    dto.IdProveedor,
                    dto.Imps.Ruc,
                    dto.Proveedor,
                    TipoPersonaMapper.FromString(dto.TipoPersona),
                    dto.Direccion1,
                    dto.Direccion2,
                    dto.Direccion3,
                    dto.Direccion4,
                    dto.Comprador,
                    new ContactoProveedor(
                        dto.Contacto.Contacto,
                        dto.Contacto.Telefono,
                        dto.Contacto.CorreoExt,
                        dto.Contacto.CorreoInt
                    ),
                    EstadoProveedorMapper.FromString(dto.Estado)
                )
            )
            .ForAllMembers(opts => opts.Ignore()); // Ignora el mapeo automático de propiedades, ya que se hace en el ConstructUsing
    }
}

