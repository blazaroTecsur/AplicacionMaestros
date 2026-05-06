namespace AplicacionMaestro.Infrastructure.Persistence.Entities.Empleados;

public class EmpleadoEntity
{
    public int IdEmpleado { get; set; }
    public string IdEmpleadoExternal { get; set; } = default!;
    public string Codigo { get; set; } = default!;
    public string NombreCompleto { get; set; } = default!;
    public string? Apellido { get; set; }
    public string? Nombre { get; set; }
    public string? Alias { get; set; }
    public string? Cargo { get; set; }
    public string? Departamento { get; set; }
    public string? Estado { get; set; }
    public string? Turno { get; set; }
    public string? Categoria { get; set; }
    public string? IdUsuario { get; set; }
    public string? FrecuenciaPago { get; set; }
    public string? TipoEmpleado { get; set; }
    public string? GeneraNomina { get; set; }
    public string? CuentaSueldo { get; set; }
    public string? PrimerNombre { get; set; }
    public string? SegundoNombre { get; set; }
    public string? PrimerApellido { get; set; }
    public string? SegundoApellido { get; set; }
    public string? Direccion1 { get; set; }
    public string? Direccion2 { get; set; }
    public string? Direccion3 { get; set; }
    public string? Direccion4 { get; set; }
    public string? Ciudad { get; set; }
    public string? CodProvincia { get; set; }
    public string? CP { get; set; }
    public string? Municipio { get; set; }
    public string? Telefono { get; set; }
    public string? TelComercial { get; set; }
    public string? ExtensionTel { get; set; }
    public string? CorreoElect { get; set; }
    public string? Correo { get; set; }
    public DateTime? FechaContratacion { get; set; }
    public DateTime? FechaRevision { get; set; }
    public DateTime? FechaRescision { get; set; }
    public string? UsuarioRegistro { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string? UsuarioModificacion { get; set; }
    public DateTime? FechaModificacion { get; set; }

    //public ICollection<EmpleadoAptitudEntity> EmpleadoAptitudes { get; set; } = null!;
    //public ICollection<EmpleadoCertificacionEntity> EmpleadoCertificaciones { get; set; } = null!;
}
