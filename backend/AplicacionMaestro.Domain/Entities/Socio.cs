using AplicacionMaestro.Domain.ValueObject;

namespace AplicacionMaestro.Domain.Entities
{
    public class Socio
    {
        public int IdSocioExternal { get; private set; }
        public string CodigoSocio { get; private set; } = null!;
        public string TipoEmpleado { get; private set; } = null!;
        public string NroReferencia { get; private set; } = null!;
        public NombreCompleto Nombre { get; private set; } = null!;
        public string? Supervisor { get; private set; }
        public string? CodigoTrabajo { get; private set; }
        public string? TipoPago { get; private set; }
        public bool Activo { get; private set; }
        public SocioGeneral General { get; private set; } = null!;
        public DateTime FechaCreacion { get; private set; }
        public DateTime? FechaModificacion { get; private set; }

        //private readonly List<Certificado> _certificaciones = new ();
        //public IReadOnlyCollection<Certificado> Certificaciones => _certificaciones;

        //private readonly List<Aptitud> _aptitudes = new ();
        //public IReadOnlyCollection<Aptitud> Aptitudes => _aptitudes;

        private Socio() { } // EF / serialización

        private Socio(
            int idExternal,
            string codigoSocio,
            string tipoEmpleado,
            string nroReferencia,
            string nombre,
            string? supervisor,
            string? codTrabajo,
            string? tipoPago,
            bool activo,
            SocioGeneral general,
            DateTime fechaCreacion)
        {
            IdSocioExternal = idExternal;
            CodigoSocio = codigoSocio;
            TipoEmpleado = tipoEmpleado;
            NroReferencia = nroReferencia;
            Nombre = NombreCompleto.Crear(nombre);
            Supervisor = supervisor;
            CodigoTrabajo = codTrabajo;
            TipoPago = tipoPago;
            Activo = activo;
            General = general;
            FechaCreacion = fechaCreacion;
        }

        public static Socio Crear(
            int idExternal,
            string codigo,
            string tipoEmpleado,
            string nroReferencia,
            string nombreCompleto,
            string? supervisor,
            string? codTrabajo,
            string? tipoPago,
            bool activo,
            SocioGeneral general,
            //IEnumerable<Certificado> certificaciones,
            //IEnumerable<Aptitud> aptitudes,
            DateTime fechaCreacion)
        {
            if (idExternal <= 0)
                throw new ArgumentException("Id del Socio requerido");
            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException("Código del socio requerido");

            var socio =  new Socio(
                idExternal,
                codigo,
                tipoEmpleado,
                nroReferencia,
                nombreCompleto,
                supervisor,
                codTrabajo,
                tipoPago,
                activo,
                general,
                fechaCreacion);
            //socio._certificaciones.AddRange(certificaciones ?? Enumerable.Empty<Certificado>());
            //socio._aptitudes.AddRange(aptitudes ?? Enumerable.Empty<Aptitud>());

            return socio;
        }

        public void Actualizar(
            string nombre,
            string tipoEmpleado,
            string? supervisor,

            bool activo,
            DateTime fechaModificacion)
        {
            Nombre = NombreCompleto.Crear(nombre);
            TipoEmpleado = tipoEmpleado;
            Supervisor = supervisor;

            Activo = activo;
            FechaModificacion = fechaModificacion;
        }
    }
}
