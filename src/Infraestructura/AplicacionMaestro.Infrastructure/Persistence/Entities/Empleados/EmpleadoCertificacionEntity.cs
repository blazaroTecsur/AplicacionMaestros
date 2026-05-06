using AplicacionMaestro.Infrastructure.Persistence.Entities.Aptitudes;
using AplicacionMaestro.Infrastructure.Persistence.Entities.Certificaciones;

namespace AplicacionMaestro.Infrastructure.Persistence.Entities.Empleados;

public class EmpleadoCertificacionEntity
{
    public int IdEmpleadoCertificacion { get; set; }

    public int IdEmpleado { get; set; }

    public int IdCertificacion { get; set; }
    public EmpleadoEntity Empleado { get; set; } = new();
    public CertificadoEntity Certificacion { get; set; } = new();
    public string? UsuarioRegistro { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string? UsuarioModificacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
}