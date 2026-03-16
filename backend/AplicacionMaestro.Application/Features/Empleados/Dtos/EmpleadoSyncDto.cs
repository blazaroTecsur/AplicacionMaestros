namespace AplicacionMaestro.Application.Features.Empleados.Dtos
{
    public class EmpleadoSyncDto
    {
        public string IdEmpleado { get; set; } = default!;
        public string Codigo { get; set; } = default!;

        public string Empleado { get; set; } = default!;

        public PrincipalDto Principal { get; set; } = new();

        public NameTaxDto NameTax { get; set; } = new();

        public ContactoEmpleadoDto Contacto { get; set; } = new();

        public RecursosHumanosDto Rh { get; set; } = new();

        //public List<CertificacionDto> Certificacion { get; set; } = new();

        //public List<AptitudDto> Aptitudes { get; set; } = new();
    }
    public class PrincipalDto
    {
        public string Apellido { get; set; } = default!;
        public string Nombre { get; set; } = default!;
        public string Alias { get; set; } = default!;
        public string Cargo { get; set; } = default!;
        public string Dpto { get; set; } = default!;
        public string Estado { get; set; } = default!;
        public string Turno { get; set; } = default!;
        public string Categoria { get; set; } = default!;
        public string IdUsuario { get; set; } = default!;
        public string FrecPago { get; set; } = default!;
        public string TipEmp { get; set; } = default!;
        public string GenNomina { get; set; } = default!;
        public string CtaSueldo { get; set; } = default!;
    }
    public class CertificacionDto
    {
        public string Codigo { get; set; } = default!;
        public string Descripcion { get; set; } = default!;
    }
    public class AptitudDto
    {
        public string Codigo { get; set; } = default!;
        public string Descripcion { get; set; }
    }
    public class NameTaxDto
    {
        public string PrNombre { get; set; } = default!;
        public string SgNombre { get; set; } = default!;
        public string PrApellido { get; set; } = default!;
        public string SgApellido { get; set; } = default!;
    }
    public class ContactoEmpleadoDto
    {
        public string Direccion1 { get; set; } = default!;
        public string Direccion2 { get; set; } = default!;
        public string Direccion3 { get; set; } = default!;
        public string Direccion4 { get; set; } = default!;
        public string Ciudad { get; set; } = default!;
        public string CodProvincia { get; set; } = default!;
        public string CP { get; set; } = default!;
        public string Municipio { get; set; } = default!;
        public string Telefono { get; set; } = default!;
        public string TelComercial { get; set; } = default!;
        public string ExtensionTel { get; set; } = default!;
        public string CorreoElect { get; set; } = default!;
        public string Correo { get; set; } = default!;
    }
    public class RecursosHumanosDto
    {
        public string FechContr { get; set; } = default!;
        public string? FechRevis { get; set; }
        public string? FechRescis { get; set; }
    }
}
