using FluentValidation;
using AplicacionMaestro.Application.Features.Almacenes.Dtos;

namespace AplicacionMaestro.Application.Features.Almacenes.Validators;

public class AlmacenSyncDtoValidator : AbstractValidator<AlmacenSyncDto>
{
    public AlmacenSyncDtoValidator()
    {
        RuleFor(x => x.IdAlmacen).NotEmpty().WithMessage("El Id del almacén es requerido.");
        RuleFor(x => x.Codigo).NotEmpty().WithMessage("El código del almacén es requerido.");
        RuleFor(x => x.Almacen).NotEmpty().WithMessage("El nombre del almacén es requerido.");
        RuleFor(x => x.Direccion1).NotEmpty().WithMessage("La dirección es requerida.");
        RuleFor(x => x.Ciudad).NotEmpty().WithMessage("La ciudad es requerida.");
        RuleFor(x => x.CodProvincia).NotEmpty().WithMessage("El código de provincia es requerido.");
        RuleFor(x => x.CP).NotEmpty().WithMessage("El código postal es requerido.");
        RuleFor(x => x.Contacto).NotEmpty().WithMessage("El contacto es requerido.");
        RuleFor(x => x.Telefono).NotEmpty().WithMessage("El teléfono es requerido.");
        RuleFor(x => x.IvaUe).NotNull().WithMessage("Los datos de IVA UE son requeridos.")
            .SetValidator(new IvaUeDtoValidator()!);
    }
}

public class IvaUeDtoValidator : AbstractValidator<IvaUeDto>
{
    public IvaUeDtoValidator()
    {
        RuleFor(x => x.VatId).NotEmpty().WithMessage("El VatId es requerido.");
    }
}
