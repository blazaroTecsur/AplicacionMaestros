using AplicacionMaestro.Application.Features.Almacenes.Dtos;
using AplicacionMaestro.Domain.Entities;
using AutoMapper;

namespace AplicacionMaestro.Application.Mapping
{
    public class AlmacenProfile : Profile
    {
        public AlmacenProfile() 
        {
            CreateMap<AlmacenSyncDto, Almacen>()
                .ConstructUsing(dto => Almacen.Create(
                    dto.IdAlmacen,
                    dto.Codigo,
                    dto.Almacen,
                    dto.Direccion1,
                    dto.Direccion2,
                    dto.Direccion3,
                    dto.Direccion4,
                    dto.Ciudad,
                    dto.CodProvincia,
                    dto.CP,
                    dto.Contacto,
                    dto.Telefono,
                    dto.Fax,
                    dto.IvaUe.VatId
                )
                )
                .ForAllMembers(opts => opts.Ignore());
        }
    }
}
