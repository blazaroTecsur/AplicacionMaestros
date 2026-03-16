using AplicacionMaestro.Infrastructure.Persistence.Entities.Socios;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplicacionMaestro.Infrastructure.Persistence.Entities.Empleados;

[Table("empleado")]
public class EmpleadoEntity
{
    [Key]
    [Column("IdEmpleado")]
    public int IdEmpleado { get; set; }

    [Column("IdEmpleadoExternal")]
    public string IdEmpleadoExternal { get; set; } = default!;

    [Column("Codigo")]
    public string Codigo { get; set; } = default!;

    [Column("NombreCompleto")]
    public string NombreCompleto { get; set; } = default!;

    // principal
    [Column("Apellido")]
    public string? Apellido { get; set; }

    [Column("Nombre")]
    public string? Nombre { get; set; }

    [Column("Alias")]
    public string? Alias { get; set; }

    [Column("Cargo")]
    public string? Cargo { get; set; }

    [Column("Dpto")]
    public string? Departamento { get; set; }

    [Column("Estado")]
    public string? Estado { get; set; }

    [Column("Turno")]
    public string? Turno { get; set; }

    [Column("Categoria")]
    public string? Categoria { get; set; }

    [Column("IdUsuario")]
    public string? IdUsuario { get; set; }

    [Column("FrecPago")]
    public string? FrecuenciaPago { get; set; }

    [Column("TipEmp")]
    public string? TipoEmpleado { get; set; }

    [Column("GenNomina")]
    public string? GeneraNomina { get; set; }

    [Column("CtaSueldo")]
    public string? CuentaSueldo { get; set; }

    // nameTax
    [Column("PrimerNombre")]
    public string? PrimerNombre { get; set; }

    [Column("SegundoNombre")]
    public string? SegundoNombre { get; set; }

    [Column("PrimerApellido")]
    public string? PrimerApellido { get; set; }

    [Column("SegundoApellido")]
    public string? SegundoApellido { get; set; }

    // contacto
    [Column("Direccion1")]
    public string? Direccion1 { get; set; }

    [Column("Direccion2")]
    public string? Direccion2 { get; set; }

    [Column("Direccion3")]
    public string? Direccion3 { get; set; }

    [Column("Direccion4")]
    public string? Direccion4 { get; set; }

    [Column("Ciudad")]
    public string? Ciudad { get; set; }

    [Column("CodProvincia")]
    public string? CodProvincia { get; set; }

    [Column("CP")]
    public string? CP { get; set; }

    [Column("Municipio")]
    public string? Municipio { get; set; }

    [Column("Telefono")]
    public string? Telefono { get; set; }

    [Column("TelComercial")]
    public string? TelComercial { get; set; }

    [Column("ExtensionTel")]
    public string? ExtensionTel { get; set; }

    [Column("CorreoElect")]
    public string? CorreoElect { get; set; }

    [Column("Correo")]
    public string? Correo { get; set; }

    // recursos humanos

    [Column("FechaContr")]
    public DateTime? FechaContratacion { get; set; }

    [Column("FechaRevis")]
    public DateTime? FechaRevision { get; set; }

    [Column("FechaRescis")]
    public DateTime? FechaRescision { get; set; }


    //auditoria

    [Column("UsuarioReg")]
    public string? UsuarioRegistro { get; set; }

    [Column("FechaReg")]
    public DateTime FechaCreacion { get; set; }

    [Column("UsuarioAct")]
    public string? UsuarioModificacion { get; set; }

    [Column("FechaAct")]
    public DateTime? FechaModificacion { get; set; }

    //public ICollection<EmpleadoAptitudEntity> EmpleadoAptitudes { get; set; } = null!;
    //public ICollection<EmpleadoCertificacionEntity> EmpleadoCertificaciones { get; set; } = null!;
}
