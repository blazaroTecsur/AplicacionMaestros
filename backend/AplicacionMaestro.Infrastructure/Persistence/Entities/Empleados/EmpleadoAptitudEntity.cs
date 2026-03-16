using AplicacionMaestro.Infrastructure.Persistence.Entities.Aptitudes;

namespace AplicacionMaestro.Infrastructure.Persistence.Entities.Empleados;

public class EmpleadoAptitudEntity
{
    public int IdEmpleadoAptitud { get; set; }

    public int IdEmpleado { get; set; }

    public int IdAptitud { get; set; }
    public EmpleadoEntity Empleado { get; set; } = new();
    public AptitudEntity Aptitud { get; set; } = new();
    public string? UsuarioRegistro { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string? UsuarioModificacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
}