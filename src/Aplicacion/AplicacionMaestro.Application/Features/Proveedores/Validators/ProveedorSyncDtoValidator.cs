using FluentValidation;

namespace AplicacionMaestro.Application.Features.Proveedores.Validators;

public class ProveedorSyncDtoValidator : AbstractValidator<ProveedorSyncDto>
{
    public ProveedorSyncDtoValidator()
    {
        RuleFor(x => x.IdProveedor).NotEmpty().WithMessage("El Id del proveedor es requerido.");
        RuleFor(x => x.Proveedor).NotEmpty().WithMessage("El nombre del proveedor es requerido.");
        RuleFor(x => x.TipoPersona).NotEmpty().WithMessage("El tipo de persona es requerido.");
        RuleFor(x => x.Direccion1).NotEmpty().WithMessage("La dirección es requerida.");
        RuleFor(x => x.Comprador).NotEmpty().WithMessage("El comprador es requerido.");
        RuleFor(x => x.Estado).NotEmpty().WithMessage("El estado es requerido.");
        RuleFor(x => x.Contacto).NotNull().WithMessage("El contacto es requerido.")
            .SetValidator(new ContactoProveedorDtoValidator()!);
        RuleFor(x => x.Imps).NotNull().WithMessage("Los datos de impuestos son requeridos.")
            .SetValidator(new ImpsDtoValidator()!);
    }
}

public class ContactoProveedorDtoValidator : AbstractValidator<ContactoProveedorDto>
{
    public ContactoProveedorDtoValidator()
    {
        RuleFor(x => x.Telefono).NotEmpty().WithMessage("El teléfono de contacto es requerido.");
        RuleFor(x => x.CorreoExt).NotEmpty().WithMessage("El correo externo es requerido.");
    }
}

public class ImpsDtoValidator : AbstractValidator<ImpsDto>
{
    public ImpsDtoValidator()
    {
        RuleFor(x => x.Ruc).NotEmpty().WithMessage("El RUC es requerido.");
    }
}
