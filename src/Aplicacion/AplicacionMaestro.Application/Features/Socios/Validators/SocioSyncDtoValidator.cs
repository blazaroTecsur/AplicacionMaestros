using FluentValidation;
using AplicacionMaestro.Application.Features.Socios.Dtos;

namespace AplicacionMaestro.Application.Features.Socios.Validators;

public class SocioSyncDtoValidator : AbstractValidator<SocioSyncDto>
{
    public SocioSyncDtoValidator()
    {
        RuleFor(x => x.IdSocio).GreaterThan(0).WithMessage("El Id del socio debe ser mayor a 0.");
        RuleFor(x => x.Socio).NotEmpty().WithMessage("El código del socio es requerido.");
        RuleFor(x => x.TipoEmpleado).NotEmpty().WithMessage("El tipo de empleado es requerido.");
        RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es requerido.");
        RuleFor(x => x.General).NotNull().WithMessage("Los datos generales son requeridos.")
            .SetValidator(new SocioGeneralDtoValidator()!);
    }
}

public class SocioGeneralDtoValidator : AbstractValidator<SocioGeneralDto>
{
    public SocioGeneralDtoValidator()
    {
        RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("El email no tiene un formato válido.");
    }
}
