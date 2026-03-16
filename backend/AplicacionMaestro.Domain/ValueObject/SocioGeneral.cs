namespace AplicacionMaestro.Domain.ValueObject
{
    public class SocioGeneral
    {
        public string? Email { get; }
        public string? DireccLocaliz { get; }
        public string? DireccMensaje { get; }
        public string? Almacen { get; }
        public string? Departamento { get; }
        public string? Usuario { get; }

        public SocioGeneral(
            string email,
            string direccLocaliz,
            string direccMensaje,
            string almacen,
            string departamento,
            string usuario)
        {
            Email = email?.Trim();
            DireccLocaliz = direccLocaliz;
            DireccMensaje = direccMensaje;
            Almacen = almacen;
            Departamento = departamento;
            Usuario = usuario?.Trim();
        }
    }
}
