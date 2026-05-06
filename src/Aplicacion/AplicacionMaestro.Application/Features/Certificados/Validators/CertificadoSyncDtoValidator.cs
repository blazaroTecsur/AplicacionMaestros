using FluentValidation;
using AplicacionMaestro.Application.Features.Certificados.Dtos;

namespace AplicacionMaestro.Application.Features.Certificados.Validators;

public class CertificadoSyncDtoValidator : AbstractValidator<CertificadoSyncDto>
{
    public CertificadoSyncDtoValidator()
    {
        RuleFor(x => x.Codigo).NotEmpty().WithMessage("El código del certificado es requerido.");
        RuleFor(x => x.Descripcion).NotEmpty().WithMessage("La descripción del certificado es requerida.");
    }
}
