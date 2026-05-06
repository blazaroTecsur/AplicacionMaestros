using FluentValidation;
using AplicacionMaestro.Application.Features.CtasContables.Dtos;

namespace AplicacionMaestro.Application.Features.CtasContables.Validators;

public class CuentaContableSyncDtoValidator : AbstractValidator<CuentaContableSyncDto>
{
    public CuentaContableSyncDtoValidator()
    {
        RuleFor(x => x.Cuenta).NotEmpty().WithMessage("La cuenta contable es requerida.");
        RuleFor(x => x.Descripcion).NotEmpty().WithMessage("La descripción es requerida.");
        RuleFor(x => x.TipoCuenta).NotEmpty().WithMessage("El tipo de cuenta es requerido.");
    }
}
