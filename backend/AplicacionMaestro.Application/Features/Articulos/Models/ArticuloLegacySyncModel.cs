namespace AplicacionMaestro.Application.Features.Articulos.Models
{
    public class ArticuloLegacySyncModel
    {
        public string? IdExternal { get; init; }
        public string? Codigo { get; init; }
        public string? Descripcion { get; init; }
        public string? UnidadMedida { get; init; }
        public string? TipoArticulo { get; init; }
        public string? Origen { get; init; }
         public string? CodigoProducto { get; init; }
         public string? CodigoAbc { get; init; }
         public bool SegLote { get; init; }
        public string? Estado { get; init; }
    }
}
