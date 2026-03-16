using AplicacionMaestro.Domain.Enums;

namespace AplicacionMaestro.Application.Common.Mappings
{
    public static class EstadoArticuloMapper
    {
        public static Estado FromExternal(string estado)
        {
            return estado.Trim().ToUpperInvariant() switch
            {
                "ACTIVO" => Estado.ACTIVO,
                "INACTIVO" => Estado.INACTIVO,
                _ => throw new ArgumentException(
                        $"Estado de artículo desconocido: {estado}")
            };
        }

        public static string ToDatabase(Estado estado)
            => estado switch
            {
                Estado.ACTIVO => "ACTIVO",
                Estado.INACTIVO => "INACTIVO",
                _ => throw new ArgumentOutOfRangeException(nameof(estado))
            };

        public static bool ToBool(Estado estado)
            => estado == Estado.ACTIVO;
    }
}
