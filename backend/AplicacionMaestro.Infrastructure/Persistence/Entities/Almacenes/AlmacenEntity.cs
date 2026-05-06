namespace AplicacionMaestro.Infrastructure.Persistence.Entities.Almacenes;

public class AlmacenEntity
{
    public int IdAlmacen { get; set; }
    public string IdAlmacenExternal { get; set; } = null!;
    public string CodigoAlmacen { get; set; } = null!;
    public string NombreAlmacen { get; set; } = null!;
    public string? Direccion1 { get; set; }
    public string? Direccion2 { get; set; }
    public string? Direccion3 { get; set; }
    public string? Direccion4 { get; set; }
    public string? Ciudad { get; set; }
    public string? CodigoProvincia { get; set; }
    public string? CodigoPostal { get; set; }
    public string? Contacto { get; set; }
    public string? Telefono { get; set; }
    public string? Fax { get; set; }
    public string? Ruc { get; set; }
    public string? UsuarioRegistro { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string? UsuarioModificacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
}
