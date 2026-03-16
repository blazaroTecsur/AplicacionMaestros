using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplicacionMaestro.Infrastructure.Persistence.Entities.CuentasContables
{
    public class CuentaContableEntity
    {
        [Key]
        [Column("IdCuentaContable")]
        public int IdCuentaContable { get; set; }

        [Column("Cuenta")]
        public string Cuenta { get; set; } = null!;

        [Column("Descripcion")]
        public string Descripcion { get; set; } = null!;    

        [Column("Tipo")]
        public string Tipo { get; set; } = null!;

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
