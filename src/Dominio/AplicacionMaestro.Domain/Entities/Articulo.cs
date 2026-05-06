using AplicacionMaestro.Domain.Enums;
using AplicacionMaestro.Domain.ValueObject;

namespace AplicacionMaestro.Domain.Entities
{
    public class Articulo
    {
        public int IdExternal { get; private set; }  
        public ArticuloCodigo Codigo { get; private set; } = null!;
        public string Descripcion { get; private set; } = null!;
        public string UnidadMedida { get; private set; } = null!;
        public string Tipo { get; private set; } = null!;
        public string Origen { get; private set; } = null!;
        public string CodigoProducto { get; private set; } = null!;
        public string CodigoAbc { get; private set; } = null!;
        public bool SegLote { get; private set; }
        public Estado EstadoMaterial { get; private set; }
        public DateTime FechaCreacion { get; private set; }
        public DateTime FechaModificacion { get; private set; }

        private Articulo() { } // EF / serialización

        private Articulo(
            int idExternal,
            string codigo,
            string descripcion,
            string unidadMedida,
            string tipo,
            string origen,
            string codigoProducto,
            string codigoAbc,
            bool segLote,
            Estado estadoMaterial,
            DateTime fechaCreacion,
            DateTime fechaModificacion)
        {
            IdExternal = idExternal;
            Codigo = ArticuloCodigo.Crear(codigo);
            Descripcion = descripcion;
            UnidadMedida = unidadMedida;
            Tipo = tipo;
            Origen = origen;
            CodigoProducto = codigoProducto;
            CodigoAbc = codigoAbc;
            SegLote = segLote;
            EstadoMaterial = estadoMaterial;
            FechaCreacion = fechaCreacion;
            FechaModificacion = fechaModificacion;
        }

        public static Articulo Create(
            int idExternal,
            string codigo,
            string descripcion,
            string unidadMedida,
            string tipo,
            string origen,
            string codigoProducto,
            string codigoAbc,
            bool segLote,
            Estado estadoMaterial,
            DateTime fechaCreacion,
            DateTime fechaModificacion)
        {
            if (string.IsNullOrWhiteSpace(idExternal.ToString()))
                throw new ArgumentException("Id del artículo es obligatorio");

            if (string.IsNullOrWhiteSpace(codigo.ToString()))
                throw new ArgumentException("Código es obligatorio");

            return new Articulo(
                idExternal,
                codigo,
                descripcion,
                unidadMedida,
                tipo,
                origen,
                codigoProducto,
                codigoAbc,
                segLote,
                estadoMaterial,
                fechaCreacion,
                fechaModificacion);
        }

        public bool TieneCambios(
            string codigo,
            string descripcion,
            string undMedida,
            string tipo,
            string origen,
            string codProducto,
            string codAbc,
            bool segLote,
            Estado estado)
        {
            return Codigo != ArticuloCodigo.Crear(codigo) ||
                   Descripcion != descripcion ||
                   UnidadMedida != undMedida ||
                   Tipo != tipo ||
                   Origen != origen ||
                   CodigoProducto != codProducto ||
                   CodigoAbc != codAbc ||
                   SegLote != segLote ||
                   EstadoMaterial != estado;
        }

        public void Actualizar(
            string descripcion,
            string unidadMedida,
            string tipo,
            string origen,
            string codigoProducto,
            string codigoAbc,
            bool segLote,
            Estado estadoMaterial
            )
        {
            Descripcion = descripcion;
            UnidadMedida = unidadMedida;
            Tipo = tipo;
            Origen = origen;
            CodigoProducto = codigoProducto;
            CodigoAbc = codigoAbc;
            SegLote = segLote;
            EstadoMaterial = estadoMaterial;

            FechaModificacion = DateTime.UtcNow;
        }
    }
}
