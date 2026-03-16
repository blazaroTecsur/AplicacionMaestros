using AplicacionMaestro.Infrastructure.Persistence.Entities.Socios;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplicacionMaestro.Infrastructure.Persistence.Entities.Certificaciones
{
    public class CertificadoEntity
    {
        [Key]
        [Column("IdCertificacion")]
        public int IdCertificacion { get; set; }

        [Column("Codigo")]
        public string Codigo { get; set; }

        [Column("Descripcion")]
        public string Descripcion { get; set; }

        [Column("UsuarioReg")]
        public string? UsuarioRegistro { get; set; }

        [Column("FechaReg")]
        public DateTime FechaCreacion { get; set; }

        [Column("UsuarioAct")]
        public string? UsuarioModificacion { get; set; }

        [Column("FechaAct")]
        public DateTime? FechaModificacion { get; set; }
    }
}
