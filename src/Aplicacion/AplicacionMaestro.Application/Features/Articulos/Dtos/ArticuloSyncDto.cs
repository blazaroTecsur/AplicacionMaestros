namespace AplicacionMaestro.Application
{
    public class ArticuloSyncDto
    {
        public int IdArticulo { get; set; }   // → idExternal / IdMatricula
        public string Codigo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string UndMedida { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public string Origen { get; set; } = null!;
        public string CodProducto { get; set; } = null!;
        public string CodAbc { get; set; } = null!;
        public bool SegLote { get; set; }
        public string EstadoMaterial { get; set; } = null!;
        //public DateTime FechaCreacion { get; set; }
        //public DateTime FechaModificacion { get; set; }
    }
}
