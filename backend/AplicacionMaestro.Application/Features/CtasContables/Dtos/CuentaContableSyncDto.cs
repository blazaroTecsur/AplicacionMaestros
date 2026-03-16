namespace AplicacionMaestro.Application.Features.CtasContables.Dtos
{
    public class CuentaContableSyncDto
    {
        public string Cuenta { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string TipoCuenta { get; set; } = null!;
    }
}
