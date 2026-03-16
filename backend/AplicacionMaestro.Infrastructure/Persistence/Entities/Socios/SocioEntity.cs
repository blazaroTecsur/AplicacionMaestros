using AplicacionMaestro.Infrastructure.Persistence.Entities.Socios;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplicacionMaestro.Infrastructure;

[Table("socio")]
public class SocioEntity
{
    [Key]
    [Column("IdSocio")]
    public int IdSocio { get; set; }

    [Column("IdSocioExternal")]
    public int IdSocioExternal { get; set; }

    [Column("CodigoSocio")]
    public string CodigoSocio { get; set; } = null!;

    [Column("TipoEmpleado")]
    public string TipoEmpleado { get; set; } = null!;

    [Column("NroReferencia")]
    public string NroReferencia { get; set; } = null!;

    [Column("NombreCompleto")]
    public string NombreCompleto { get; set; } = null!;

    [Column("Supervisor")]
    public string? Supervisor { get; set; }

    [Column("CodTrabajo")]
    public string? CodTrabajo { get; set; }

    [Column("TipoPago")]
    public string? TipoPago { get; set; }

    [Column("Email")]
    public string? Email { get; set; }

    [Column("DireccionLocaliz")]
    public string? DireccionLocaliz { get; set; }

    [Column("DireccionMensaje")]
    public string? DireccionMensaje { get; set; }

    [Column("Almacen")]
    public string? Almacen { get; set; }

    [Column("Departamento")]
    public string? Departamento { get; set; }

    [Column("Usuario")]
    public string? Usuario { get; set; }

    [Column("Activo")]
    public bool Activo { get; set; }

    [Column("UsuarioReg")]
    public string? UsuarioRegistro { get; set; }

    [Column("FechaReg")]
    public DateTime FechaCreacion { get; set; }

    [Column("UsuarioAct")]
    public string? UsuarioModificacion { get; set; }

    [Column("FechaAct")]
    public DateTime? FechaModificacion { get; set; }

    //public ICollection<SocioAptitudEntity> SocioAptitudes { get; set; } = null!;
    //public ICollection<SocioCertificacionEntity> SocioCertificaciones { get; set; } = null!;
}
