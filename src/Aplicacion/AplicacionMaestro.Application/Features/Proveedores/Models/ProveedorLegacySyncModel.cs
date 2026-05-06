namespace AplicacionMaestro.Application.Features.Proveedores.Models
{
    public class ProveedorLegacySyncModel
    {
        public string IdExternal { get; init; } = null!;
        public string? Ruc { get; init; }
        public string? RazonSocial { get; init; }
        public string? TipoPersona { get; init; }

        public string Direccion1 { get; init; } = null!;
        public string? Direccion2 { get; init; }
        public string? Direccion3 { get; init; }
        public string? Direccion4 { get; init; }

        public string? Comprador { get; init; }

        public string ContactoNombre { get; init; } = null!;
        public string ContactoTelefono { get; init; } = null!;
        public string CorreoExt { get; init; } = null!;
        public string? CorreoInt { get; init; }

        public string Estado { get; init; } = null!;

    }


}
