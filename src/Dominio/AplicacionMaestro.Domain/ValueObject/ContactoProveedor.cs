namespace AplicacionMaestro.Domain.ValueObject
{
    public class ContactoProveedor
    {
        public string Nombre { get; }
        public string Telefono { get; }
        public string CorreoExterno { get; }
        public string? CorreoInterno { get; }

        public ContactoProveedor(
            string nombre,
            string telefono,
            string correoExterno,
            string correoInterno)
        {
            Nombre = nombre;
            Telefono = telefono;
            CorreoExterno = correoExterno;
            CorreoInterno = correoInterno;
        }
    }

}
