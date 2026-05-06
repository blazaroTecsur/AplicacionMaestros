namespace AplicacionMaestro.Infrastructure;

public class ProveedorEntity
{
    public int IdProveedor { get; set; }
    public int IdExternal { get; set; }
    public string TipoPersona { get; set; } = null!;
    public string Ruc { get; set; } = null!;
    public string RazonSocial { get; set; } = null!;
    public string Direccion1 { get; set; } = null!;
    public string Direccion2 { get; set; } = null!;
    public string Direccion3 { get; set; } = null!;
    public string Direccion4 { get; set; } = null!;
    public string Comprador { get; set; } = null!;
    public string? Contacto { get; set; }
    public string Telefono { get; set; } = null!;
    public string CorreoExterno { get; set; } = null!;
    public string CorreoInterno { get; set; } = null!;
    public string Estado { get; set; } = null!;
    public string? UsuarioRegistro { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string? UsuarioModificacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
}
