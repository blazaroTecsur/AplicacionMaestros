using FluentValidation;

namespace AplicacionMaestro.Application.Features.Articulos.Validators;

public class ArticuloSyncDtoValidator : AbstractValidator<ArticuloSyncDto>
{
    public ArticuloSyncDtoValidator()
    {
        RuleFor(x => x.IdArticulo).GreaterThan(0).WithMessage("El Id del artículo debe ser mayor a 0.");
        RuleFor(x => x.Codigo).NotEmpty().WithMessage("El código del artículo es requerido.");
        RuleFor(x => x.Descripcion).NotEmpty().WithMessage("La descripción es requerida.");
        RuleFor(x => x.UndMedida).NotEmpty().WithMessage("La unidad de medida es requerida.");
        RuleFor(x => x.Tipo).NotEmpty().WithMessage("El tipo es requerido.");
        RuleFor(x => x.Origen).NotEmpty().WithMessage("El origen es requerido.");
        RuleFor(x => x.CodProducto).NotEmpty().WithMessage("El código de producto es requerido.");
        RuleFor(x => x.CodAbc).NotEmpty().WithMessage("El código ABC es requerido.");
        RuleFor(x => x.EstadoMaterial).NotEmpty().WithMessage("El estado del material es requerido.");
    }
}
