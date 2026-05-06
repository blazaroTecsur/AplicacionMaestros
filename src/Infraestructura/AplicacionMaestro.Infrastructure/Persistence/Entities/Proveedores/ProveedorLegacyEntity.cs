using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplicacionMaestro.Infrastructure;

[Table("entidad")]
public class ProveedorLegacyEntity
{
    // ======================
    // 🔑 IDENTIDAD
    // ======================
    [Key]
    [Column("IdEntidad")]
    public int IdEntidad { get; set; }

    [Column("Codigo")]
    public string Ruc { get; set; } = null!;

    [Column("RazonSocial")]
    public string RazonSocial { get; set; } = null!;

    [Column("Sigla")]
    public string? Sigla { get; set; }

    [Column("Direccion")]
    public string? Direccion { get; set; }

    [Column("Telefono")]
    public string? Telefono { get; set; }

    [Column("Correo")]
    public string? Correo { get; set; }

    [Column("CorreoRacs")]
    public string? CorreoRacs { get; set; }

    [Column("Actividad")]
    public string? Actividad { get; set; }

    [Column("Estado")]
    public bool Estado { get; set; }

    [Column("EsCliente")]
    public bool EsCliente { get; set; }

    [Column("EsProveedor")]
    public bool EsProveedor { get; set; }

    [Column("EsContratista")]
    public bool EsContratista { get; set; }

    [Column("EsCorporativo")]
    public bool EsCorporativo { get; set; }

    [Column("UsuarioReg")]
    public string? UsuarioRegistro { get; set; }

    [Column("FechaReg")]
    public DateTime FechaCreacion { get; set; }

    [Column("UsuarioAct")]
    public string? UsuarioModifiacion { get; set; }

    [Column("FechaAct")]
    public DateTime? FechaModificacion { get; set; }

}