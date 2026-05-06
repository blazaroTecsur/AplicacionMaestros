using FluentValidation;
using AplicacionMaestro.Application.Features.Aptitudes.Dtos;

namespace AplicacionMaestro.Application.Features.Aptitudes.Validators;

public class AptitudSyncDtoValidator : AbstractValidator<AptitudSyncDto>
{
    public AptitudSyncDtoValidator()
    {
        RuleFor(x => x.Codigo).NotEmpty().WithMessage("El código de la aptitud es requerido.");
        RuleFor(x => x.Descripcion).NotEmpty().WithMessage("La descripción de la aptitud es requerida.");
    }
}
