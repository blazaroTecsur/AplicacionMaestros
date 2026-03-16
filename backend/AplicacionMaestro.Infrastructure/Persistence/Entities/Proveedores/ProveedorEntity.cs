using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplicacionMaestro.Infrastructure;

[Table("proveedor")]
public class ProveedorEntity
{
    [Key]
    [Column("IdProveedor")]
    public int IdProveedor { get; set; }

    [Column("IdProveedorExternal")]
    public int IdExternal { get; set; }

    [Column("TipoPersona")]
    public string TipoPersona { get; set; } = null!;

    [Column("Ruc")]
    public string Ruc { get; set; } = null!;

    [Column("NombreProveedor")]
    public string RazonSocial { get; set; } = null!;

    [Column("Direccion1")]
    public string Direccion1 { get; set; } = null!;

    [Column("Direccion2")]
    public string Direccion2 { get; set; } = null!;

    [Column("Direccion3")]
    public string Direccion3 { get; set; } = null!;

    [Column("Direccion4")]
    public string Direccion4 { get; set; } = null!;

    [Column("Comprador")]
    public string Comprador { get; set; } = null!;

    [Column("Contacto")]
    public string? Contacto { get; set; } = null!;

    [Column("TelefonoContacto")]
    public string Telefono { get; set; } = null!;

    [Column("CorreoExternoContacto")]
    public string CorreoExterno { get; set; } = null!;

    [Column("CorreoInternoContacto")]
    public string CorreoInterno { get; set; } = null!;

    [Column("Estado")]
    public string Estado { get; set; } = null!;

    [Column("UsuarioReg")]
    public string? UsuarioRegistro { get; set; }

    [Column("FechaReg")]
    public DateTime FechaCreacion { get; set; }

    [Column("UsuarioAct")]
    public string? UsuarioModificacion { get; set; }

    [Column("FechaAct")]
    public DateTime? FechaModificacion { get; set; }
}
