namespace AplicacionMaestro.Infrastructure.Persistence.Entities.CuentasContables;

public class CuentaContableEntity
{
    public int IdCuentaContable { get; set; }
    public string Cuenta { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public string Tipo { get; set; } = null!;
    public string? UsuarioRegistro { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string? UsuarioModificacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
}
