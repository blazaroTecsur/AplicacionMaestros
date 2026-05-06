namespace AplicacionMaestro.Domain.Models
{
    public class ContactoInfo
    {
        public string Direccion1 { get; }
        public string Direccion2 { get; }
        public string Direccion3 { get; }
        public string Direccion4 { get; }
        public string Ciudad { get; }
        public string Provincia { get; }
        public string CodigoPostal { get; }
        public string Municipio { get; }
        public string Telefono { get; }
        public string TelefonoComercial { get; }
        public string ExtensionTelefono { get; }
        public string CorreoElectronico { get; }
        public string Correo { get; }

        public ContactoInfo(
            string direccion1,
            string direccion2,
            string direccion3,
            string direccion4,
            string ciudad,
            string provincia,
            string codigoPostal,
            string municipio,
            string telefono,
            string telefonoComercial,
            string extensionTelefono,
            string correoElectronico,
            string correo)
        {
            Direccion1 = direccion1;
            Direccion2 = direccion2;
            Direccion3 = direccion3;
            Direccion4 = direccion4;
            Ciudad = ciudad;
            Provincia = provincia;
            CodigoPostal = codigoPostal;
            Municipio = municipio;
            Telefono = telefono;
            TelefonoComercial = telefonoComercial;
            ExtensionTelefono = extensionTelefono;
            CorreoElectronico = correoElectronico;
            Correo = correo;
        }
    }
}
