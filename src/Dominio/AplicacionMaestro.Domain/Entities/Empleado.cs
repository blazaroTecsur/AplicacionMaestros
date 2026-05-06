using AplicacionMaestro.Domain.Models;

namespace AplicacionMaestro.Domain.Entities
{
    public class Empleado
    {
        public string IdEmpleadoExternal { get; private set; }
        public string Codigo { get; private set; }

        public string NombreCompleto { get; private set; }

        public PrincipalInfo Principal { get; private set; }

        public NameTaxInfo NameTax { get; private set; }

        public ContactoInfo Contacto { get; private set; }

        public RecursosHumanosInfo RecursosHumanos { get; private set; }

        //public IReadOnlyCollection<Certificado> Certificaciones => _certificaciones;
        //private readonly List<Certificado> _certificaciones = new();

        //public IReadOnlyCollection<Aptitud> Aptitudes => _aptitudes;
        //private readonly List<Aptitud> _aptitudes = new();

        private Empleado() { }

        public Empleado(
            string idEmpleadoExternal,
            string codigo,
            string nombreCompleto,
            PrincipalInfo principal,
            NameTaxInfo nameTax,
            ContactoInfo contacto,
            RecursosHumanosInfo recursosHumanos)
        {
            IdEmpleadoExternal = idEmpleadoExternal;
            Codigo = codigo;
            NombreCompleto = nombreCompleto;
            Principal = principal;
            NameTax = nameTax;
            Contacto = contacto;
            RecursosHumanos = recursosHumanos;

        }
        public static Empleado Crear(
            string idEmpleadoExternal,
            string codigo,
            string nombreCompleto,
            PrincipalInfo principal,
            NameTaxInfo nameTax,
            ContactoInfo contacto,
            RecursosHumanosInfo recursosHumanos)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException("El código del empleado es obligatorio");

            if (string.IsNullOrWhiteSpace(nombreCompleto))
                throw new ArgumentException("El nombre del empleado es obligatorio");

            var empleado = new Empleado(
                idEmpleadoExternal,
                codigo,
                nombreCompleto,
                principal,
                nameTax,
                contacto,
                recursosHumanos
            );

            //if (certificaciones != null)
            //    empleado._certificaciones.AddRange(certificaciones);

            //if (aptitudes != null)
            //    empleado._aptitudes.AddRange(aptitudes);

            return empleado;
        }
    }
}
