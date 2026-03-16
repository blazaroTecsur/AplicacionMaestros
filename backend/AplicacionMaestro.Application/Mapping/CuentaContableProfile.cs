using AplicacionMaestro.Application.Features.CtasContables.Dtos;
using AplicacionMaestro.Domain.Entities;
using AutoMapper;

namespace AplicacionMaestro.Application.Mapping
{
    public class CuentaContableProfile : Profile
    {
        public CuentaContableProfile()
        {
            CreateMap<CuentaContableSyncDto, CuentaContable>()
                .ConstructUsing(dto => new CuentaContable(
                    dto.Cuenta,
                    dto.Descripcion,
                    dto.TipoCuenta
                ))
                .ForAllMembers(opts => opts.Ignore());
        }
    }
}
