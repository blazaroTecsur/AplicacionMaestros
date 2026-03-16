using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplicacionMaestro.Infrastructure;

[Table("matricula")]
public class ArticuloLegacyEntity
{
    [Key]
    [Column("IdMatricula")]
    public int IdMatricula { get; set; }

    [Column("Codigo")]
    public string Codigo { get; set; } = null!;

    [Column("CodigoCorto")]
    public string CodigoCorto { get; set; } = null!;

    [Column("Marca")]
    public string Marca { get; set; } = null!;

    [Column("Descripcion")]
    public string Descripcion { get; set; } = null!;

    [Column("DescripcionLarga")]
    public string DescripcionLarga { get; set; } = null!;

    [Column("Unidad")]
    public string Unidad { get; set; } = null!;

    [Column("EsVenta")]
    public bool EsVenta { get; set; }

    [Column("Margen")]
    public decimal Margen { get; set; }

    [Column("EstadoMatricula")]
    public string EstadoMatricula { get; set; } = null!;

    [Column("EstadoMarca")]
    public string? EstadoMarca { get; set; }

    [Column("Estado")]
    public bool Estado { get; set; }

    [Column("UsuarioReg")]
    public string? UsuarioRegistro { get; set; }

    [Column("FechaReg")]
    public DateTime FechaCreacion { get; set; }

    [Column("UsuarioAct")]
    public string? UsuarioModifiacion { get; set; }

    [Column("FechaAct")]
    public DateTime? FechaModificacion { get; set; }
}
