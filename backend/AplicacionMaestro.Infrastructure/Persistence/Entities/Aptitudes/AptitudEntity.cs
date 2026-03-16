using AplicacionMaestro.Infrastructure.Persistence.Entities.Socios;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplicacionMaestro.Infrastructure.Persistence.Entities.Aptitudes
{
    public class AptitudEntity
    {
        [Key]
        [Column("IdAptitud")]
        public int IdAptitud { get; set; }

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
