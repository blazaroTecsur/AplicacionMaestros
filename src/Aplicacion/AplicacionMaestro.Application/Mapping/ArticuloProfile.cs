using AplicacionMaestro.Application.Common.Mappings;
using AplicacionMaestro.Domain.Entities;
using AutoMapper;

namespace AplicacionMaestro.Application.Mapping
{
    public class ArticuloProfile : Profile
    {
        public ArticuloProfile()
        {
            CreateMap<ArticuloSyncDto, Articulo>()
                .ConstructUsing(dto => Articulo.Create(
                    dto.IdArticulo,
                    dto.Codigo,
                    dto.Descripcion,
                    dto.UndMedida,
                    dto.Tipo,
                    dto.Origen,
                    dto.CodProducto,
                    dto.CodAbc,
                    dto.SegLote,
                    EstadoArticuloMapper.FromExternal(dto.EstadoMaterial),
                    DateTime.UtcNow,
                    DateTime.UtcNow
                )
                )
                .ForAllMembers(opts => opts.Ignore());
        }
    }
}
