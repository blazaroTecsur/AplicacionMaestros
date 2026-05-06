using AplicacionMaestro.Infrastructure.Persistence.Entities.Certificaciones;

namespace AplicacionMaestro.Infrastructure.Persistence.Entities.Socios
{
    public class SocioCertificacionEntity
    {
        public int IdSocio { get; set; }
        public int IdCertificacion { get; set; }

        public SocioEntity Socio { get; set; } = new();
        public CertificadoEntity Certificacion { get; set; } = new();

        public string? UsuarioRegistro { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
