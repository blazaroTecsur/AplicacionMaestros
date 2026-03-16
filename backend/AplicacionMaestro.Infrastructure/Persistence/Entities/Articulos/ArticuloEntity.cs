using AplicacionMaestro.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplicacionMaestro.Infrastructure;

[Table("articulo")]
public class ArticuloEntity
{
    [Key]
    [Column("IdArticulo")]
    public int IdArticulo { get; set; }

    [Column("IdArticuloExternal")]
    public int IdExternal { get; set; }

    [Column("Codigo")]
    public string Codigo { get; set; } = null!;

    [Column("Descripcion")]
    public string Descripcion { get; set; } = null!;

    [Column("UnidadMedida")]
    public string UnidadMedida { get; set; } = null!;

    [Column("Tipo")]
    public string Tipo { get; set; } = null!;

    [Column("Origen")]
    public string Origen { get; set; } = null!;

    [Column("CodigoProducto")]
    public string CodigoProducto { get; set; } = null!;

    [Column("CodigoAbc")]
    public string CodigoAbc { get; set; } = null!;

    [Column("SegLote")]
    public bool SegLote { get; set; } 

    [Column("EstadoMaterial")]
    public Estado EstadoMaterial { get; set; }

    [Column("UsuarioReg")]
    public string? UsuarioRegistro { get; set; }

    [Column("FechaReg")]
    public DateTime FechaCreacion { get; set; }

    [Column("UsuarioAct")]
    public string? UsuarioModificacion { get; set; }

    [Column("FechaAct")]
    public DateTime? FechaModificacion { get; set; }
}
