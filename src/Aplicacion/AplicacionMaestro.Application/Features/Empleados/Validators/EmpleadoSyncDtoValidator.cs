using FluentValidation;
using AplicacionMaestro.Application.Features.Empleados.Dtos;

namespace AplicacionMaestro.Application.Features.Empleados.Validators;

public class EmpleadoSyncDtoValidator : AbstractValidator<EmpleadoSyncDto>
{
    public EmpleadoSyncDtoValidator()
    {
        RuleFor(x => x.IdEmpleado).NotEmpty().WithMessage("El Id del empleado es requerido.");
        RuleFor(x => x.Codigo).NotEmpty().WithMessage("El código del empleado es requerido.");
        RuleFor(x => x.Empleado).NotEmpty().WithMessage("El nombre del empleado es requerido.");
        RuleFor(x => x.Principal).NotNull().WithMessage("Los datos principales son requeridos.")
            .SetValidator(new PrincipalDtoValidator()!);
        RuleFor(x => x.NameTax).NotNull().WithMessage("Los datos de nombre fiscal son requeridos.")
            .SetValidator(new NameTaxDtoValidator()!);
        RuleFor(x => x.Contacto).NotNull().WithMessage("Los datos de contacto son requeridos.")
            .SetValidator(new ContactoEmpleadoDtoValidator()!);
        RuleFor(x => x.Rh).NotNull().WithMessage("Los datos de recursos humanos son requeridos.")
            .SetValidator(new RecursosHumanosDtoValidator()!);
    }
}

public class PrincipalDtoValidator : AbstractValidator<PrincipalDto>
{
    public PrincipalDtoValidator()
    {
        RuleFor(x => x.Apellido).NotEmpty().WithMessage("El apellido es requerido.");
        RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es requerido.");
        RuleFor(x => x.Estado).NotEmpty().WithMessage("El estado es requerido.");
        RuleFor(x => x.TipEmp).NotEmpty().WithMessage("El tipo de empleado es requerido.");
    }
}

public class NameTaxDtoValidator : AbstractValidator<NameTaxDto>
{
    public NameTaxDtoValidator()
    {
        RuleFor(x => x.PrNombre).NotEmpty().WithMessage("El primer nombre fiscal es requerido.");
        RuleFor(x => x.PrApellido).NotEmpty().WithMessage("El primer apellido fiscal es requerido.");
    }
}

public class ContactoEmpleadoDtoValidator : AbstractValidator<ContactoEmpleadoDto>
{
    public ContactoEmpleadoDtoValidator()
    {
        RuleFor(x => x.Direccion1).NotEmpty().WithMessage("La dirección es requerida.");
        RuleFor(x => x.Correo).NotEmpty().WithMessage("El correo electrónico es requerido.");
    }
}

public class RecursosHumanosDtoValidator : AbstractValidator<RecursosHumanosDto>
{
    public RecursosHumanosDtoValidator()
    {
        RuleFor(x => x.FechContr).NotEmpty().WithMessage("La fecha de contratación es requerida.");
    }
}
