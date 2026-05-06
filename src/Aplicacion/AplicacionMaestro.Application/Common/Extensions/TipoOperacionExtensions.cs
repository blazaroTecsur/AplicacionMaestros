using AplicacionMaestro.Application.Common.Enums;

namespace AplicacionMaestro.Application.Common.Extensions
{
    public static class TipoOperacionExtensions
    {
        public static TipoOperacionProveedor ToTipoOperacionProveedor(
            this string value)
        {
            return value switch
            {
                "Proveedor.Creado" => TipoOperacionProveedor.Creado,
                "Proveedor.Modificado" => TipoOperacionProveedor.Modificado,
                _ => throw new InvalidOperationException(
                    $"TipoOperacion no soportada: {value}")
            };
        }

        public static TipoOperacionSocio ToTipoOperacionSocio(
            this string value)
        {
            return value switch
            {
                "Socio.Creado" => TipoOperacionSocio.Creado,
                "Socio.Modificado" => TipoOperacionSocio.Modificado,
                _ => throw new InvalidOperationException(
                    $"TipoOperacion no soportada: {value}")
            };
        }
    }
}
