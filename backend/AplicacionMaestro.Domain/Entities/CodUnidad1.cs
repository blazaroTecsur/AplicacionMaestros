namespace AplicacionMaestro.Domain.Entities
{
    public class CodUnidad1
    {
        public string Codigo { get; }
        public string Descripcion { get; private set; } = null!;

        public CodUnidad1(string codigo, string descripcion)
        {
            Codigo = codigo;
            Descripcion = descripcion;

            if (string.IsNullOrWhiteSpace(Codigo))
                throw new ArgumentException("Código es obligatorio", nameof(codigo));
        }
    }
}
