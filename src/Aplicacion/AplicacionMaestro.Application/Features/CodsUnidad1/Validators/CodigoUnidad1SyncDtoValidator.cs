using FluentValidation;
using AplicacionMaestro.Application.Features.CodsUnidad1.Dtos;

namespace AplicacionMaestro.Application.Features.CodsUnidad1.Validators;

public class CodigoUnidad1SyncDtoValidator : AbstractValidator<CodigoUnidad1SyncDto>
{
    public CodigoUnidad1SyncDtoValidator()
    {
        RuleFor(x => x.Codigo).NotEmpty().WithMessage("El código de unidad es requerido.");
        RuleFor(x => x.Descripcion).NotEmpty().WithMessage("La descripción de la unidad es requerida.");
    }
}
