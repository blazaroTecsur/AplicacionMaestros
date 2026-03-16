using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplicacionMaestro.Infrastructure.Persistence.Entities.Almacenes
{
    [Table("almacen")]
    public class AlmacenEntity
    {
        [Key]
        [Column("IdAlmacen")]
        public int IdAlmacen { get; set; }

        [Column("IdAlmacenExternal")]
        public string IdAlmacenExternal { get; set; } = null!;

        [Column("CodigoAlmacen")]
        public string CodigoAlmacen { get; set; } = null!;

        [Column("NombreAlmacen")]
        public string NombreAlmacen { get; set; } = null!;

        [Column("Direccion1")]
        public string? Direccion1 { get; set; }

        [Column("Direccion2")]
        public string? Direccion2 { get; set; }

        [Column("Direccion3")]
        public string? Direccion3 { get; set; }

        [Column("Direccion4")]
        public string? Direccion4 { get; set; }

        [Column("Ciudad")]
        public string? Ciudad { get; set; }

        [Column("CodigoProvincia")]
        public string? CodigoProvincia { get; set; }

        [Column("CodigoPostal")]
        public string? CodigoPostal { get; set; }

        [Column("Contacto")]
        public string? Contacto { get; set; }

        [Column("Telefono")]
        public string? Telefono { get; set; }

        [Column("Fax")]
        public string? Fax { get; set; }

        [Column("Ruc")]
        public string? Ruc { get; set; }

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
