using AplicacionMaestro.Domain.Enums;

namespace AplicacionMaestro.Application.Common.Mappings
{
    public static class EstadoProveedorMapper
    {
        public static Estado FromString(string tipo)
        {
            return tipo switch
            {
                "A" => Estado.ACTIVO,
                "I" => Estado.INACTIVO,
                _ => throw new ArgumentException("Estado inválido")
            };
        }
        public static bool MapEstadoToLegacyBool(string estado)
        {
            return estado switch
            {
                "A" => true,
                "I" => false,
                "ACTIVO" => true,
                "INACTIVO" => false,
                _ => throw new ArgumentException($"Estado inválido: {estado}")
            };
        }
    }

}
