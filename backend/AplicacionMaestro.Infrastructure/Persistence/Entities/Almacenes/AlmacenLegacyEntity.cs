using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplicacionMaestro.Infrastructure.Persistence.Entities.Almacenes
{
    [Table("almacen")]
    public class AlmacenLegacyEntity
    {
        [Key]
        [Column("IdAlmacen")]
        public int IdAlmacen { get; set; }

        [Column("Codigo")]
        public string CodigoAlmacen { get; set; } = null!;

        [Column("Nombre")]
        public string NombreAlmacen { get; set; } = null!;

        [Column("Direccion")]
        public string? Direccion { get; set; }

        [Column("Correo")]
        public string? Correo { get; set; }

        [Column("Responsable")]
        public string? Responsable { get; set; }

        [Column("Estado")]
        public bool Estado { get; set; }

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
