namespace AplicacionMaestro.Domain.Models
{
    public class PrincipalInfo
    {
        public string Apellido { get; }
        public string Nombre { get; }
        public string Alias { get; }
        public string Cargo { get; }
        public string Departamento { get; }
        public string Estado { get; }
        public string Turno { get; }
        public string Categoria { get; }
        public string IdUsuario { get; }
        public string FrecuenciaPago { get; }
        public string TipoEmpleado { get; }
        public string GeneraNomina { get; }
        public string CuentaSueldo { get; }

        public PrincipalInfo(
            string apellido,
            string nombre,
            string alias,
            string cargo,
            string departamento,
            string estado,
            string turno,
            string categoria,
            string idUsuario,
            string frecuenciaPago,
            string tipoEmpleado,
            string generaNomina,
            string cuentaSueldo)
        {
            Apellido = apellido;
            Nombre = nombre;
            Alias = alias;
            Cargo = cargo;
            Departamento = departamento;
            Estado = estado;
            Turno = turno;
            Categoria = categoria;
            IdUsuario = idUsuario;
            FrecuenciaPago = frecuenciaPago;
            TipoEmpleado = tipoEmpleado;
            GeneraNomina = generaNomina;
            CuentaSueldo = cuentaSueldo;
        }
    }
}
