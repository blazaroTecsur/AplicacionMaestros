namespace AplicacionMaestro.Domain.Models
{
    public class RecursosHumanosInfo
    {
        public DateTime? FechaContratacion { get; }
        public DateTime? FechaRevision { get; }
        public DateTime? FechaRescision { get; }

        public RecursosHumanosInfo(
            DateTime? fechaContratacion,
            DateTime? fechaRevision,
            DateTime? fechaRescision)
        {
            FechaContratacion = fechaContratacion;
            FechaRevision = fechaRevision;
            FechaRescision = fechaRescision;
        }
    }
}
