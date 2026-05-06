using AplicacionMaestro.Domain.Enums;

namespace AplicacionMaestro.Infrastructure;

public class ArticuloEntity
{
    public int IdArticulo { get; set; }
    public int IdExternal { get; set; }
    public string Codigo { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public string UnidadMedida { get; set; } = null!;
    public string Tipo { get; set; } = null!;
    public string Origen { get; set; } = null!;
    public string CodigoProducto { get; set; } = null!;
    public string CodigoAbc { get; set; } = null!;
    public bool SegLote { get; set; }
    public Estado EstadoMaterial { get; set; }
    public string? UsuarioRegistro { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string? UsuarioModificacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
}
