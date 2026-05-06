namespace AplicacionMaestro.Domain.Models
{
    public class NameTaxInfo
    {
        public string PrimerNombre { get; }
        public string SegundoNombre { get; }
        public string PrimerApellido { get; }
        public string SegundoApellido { get; }

        public NameTaxInfo(
            string primerNombre,
            string segundoNombre,
            string primerApellido,
            string segundoApellido)
        {
            PrimerNombre = primerNombre;
            SegundoNombre = segundoNombre;
            PrimerApellido = primerApellido;
            SegundoApellido = segundoApellido;
        }
    }
}
