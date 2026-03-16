namespace AplicacionMaestro.Domain.Entities
{
    public class CuentaContable
    {
        public string Cuenta { get; }
        public string Descripcion { get; private set; } = null!;
        public string Tipo { get; private set; } = null!;

        public CuentaContable(string cuenta, string descripcion, string tipo)
        {
            Cuenta = cuenta;
            Descripcion = descripcion;
            Tipo = tipo;

            if (string.IsNullOrWhiteSpace(Cuenta))
                throw new ArgumentException("Número de Cuenta es obligatorio", nameof(cuenta));
        }
    }
}
