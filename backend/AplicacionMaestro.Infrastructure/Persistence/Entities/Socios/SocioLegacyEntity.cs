using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplicacionMaestro.Infrastructure;

[Table("personal")]
public class SocioLegacyEntity
{
    [Key]
    [Column("IdPersonal")]
    public int IdPersonal { get; set; }

    [Column("Dni")]
    public string Dni { get; set; } = null!;

    [Column("IdEmpresa")]
    public int IdEmpresa { get; set; }

    [Column("Nombres")]
    public string Nombres { get; set; } = null!;

    [Column("ApePaterno")]
    public string ApePaterno { get; set; } = null!;

    [Column("ApeMaterno")]
    public string ApeMaterno { get; set; } = null!;

    [Column("Email")]
    public string? Email { get; set; }

    [Column("Estado")]
    public bool Estado { get; set; }

    [Column("UsuarioReg")]
    public string UsuarioReg { get; set; } = null!;

    [Column("FechaReg")]
    public DateTime FechaReg { get; set; }

    [Column("UsuarioAct")]
    public string? UsuarioAct { get; set; }

    [Column("FechaAct")]
    public DateTime? FechaAct { get; set; }
}
