namespace AplicacionMaestro.Infrastructure;

public class SocioEntity
{
    public int IdSocio { get; set; }
    public int IdSocioExternal { get; set; }
    public string CodigoSocio { get; set; } = null!;
    public string TipoEmpleado { get; set; } = null!;
    public string NroReferencia { get; set; } = null!;
    public string NombreCompleto { get; set; } = null!;
    public string? Supervisor { get; set; }
    public string? CodTrabajo { get; set; }
    public string? TipoPago { get; set; }
    public string? Email { get; set; }
    public string? DireccionLocaliz { get; set; }
    public string? DireccionMensaje { get; set; }
    public string? Almacen { get; set; }
    public string? Departamento { get; set; }
    public string? Usuario { get; set; }
    public bool Activo { get; set; }
    public string? UsuarioRegistro { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string? UsuarioModificacion { get; set; }
    public DateTime? FechaModificacion { get; set; }

    //public ICollection<SocioAptitudEntity> SocioAptitudes { get; set; } = null!;
    //public ICollection<SocioCertificacionEntity> SocioCertificaciones { get; set; } = null!;
}
