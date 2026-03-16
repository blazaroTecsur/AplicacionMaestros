using AplicacionMaestro.Infrastructure.Persistence.Entities.Aptitudes;

namespace AplicacionMaestro.Infrastructure.Persistence.Entities.Socios
{
    public class SocioAptitudEntity
    {
        public int IdSocio { get; set; }
        public int IdAptitud { get; set; }

        public SocioEntity Socio { get; set; } = new ();
        public AptitudEntity Aptitud { get; set; } = new();
        public string? UsuarioRegistro { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
