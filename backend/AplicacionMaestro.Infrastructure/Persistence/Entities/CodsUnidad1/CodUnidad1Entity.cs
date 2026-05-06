namespace AplicacionMaestro.Infrastructure.Persistence.Entities.CodsUnidad1;

public class CodUnidad1Entity
{
    public int IdCodUnidad1 { get; set; }
    public string Codigo { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public string? UsuarioRegistro { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string? UsuarioModificacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
}
