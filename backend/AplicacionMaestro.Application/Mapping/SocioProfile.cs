using AplicacionMaestro.Application.Features.Socios.Dtos;
using AplicacionMaestro.Domain.Entities;
using AplicacionMaestro.Domain.ValueObject;
using AutoMapper;

namespace AplicacionMaestro.Application.Mapping
{
    public class SocioProfile : Profile
    {
        public SocioProfile()
        {
            CreateMap<SocioSyncDto, Socio>()
                .ConstructUsing(dto => Socio.Crear(
                    dto.IdSocio,                     
                    dto.Socio,
                    dto.TipoEmpleado,
                    dto.NroRef,
                    dto.Nombre,                      
                    dto.Supervisor, 
                    dto.CodTrabajo,
                    dto.TipoPago,
                    dto.Activo,
                    MapGeneral(dto.General),
                    DateTime.UtcNow
                )).ForAllMembers(opts => opts.Ignore());
        }
        private static SocioGeneral MapGeneral(SocioGeneralDto dto)
        {
            if (dto == null)
                return null;

            return new SocioGeneral(
                dto.Email,
                dto.DireccLocaliz,
                dto.DireccMensaje,
                dto.Almacen,
                dto.Departamento,
                dto.Usuario
            );
        }
    }
}
