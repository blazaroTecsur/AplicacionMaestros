using AplicacionMaestro.Domain.Exceptions;

namespace AplicacionMaestro.Domain.ValueObject
{
    public sealed class ArticuloCodigo
    {
        public string Valor { get; }
        public string CodigoCorto { get; }
        public string Marca { get; }

        private ArticuloCodigo(string valor)
        {
            Valor = valor;
            CodigoCorto = valor[..^3];
            Marca = valor[^3..];
        }

        public static ArticuloCodigo Crear(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new DomainException("El código del artículo es obligatorio");

            if (codigo.Length < 3)
                throw new DomainException("El código debe tener al menos 3 caracteres");

            return new ArticuloCodigo(codigo.Trim());
        }
    }

}
