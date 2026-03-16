namespace AplicacionMaestro.Application;

public class ProveedorSyncDto
{
    public string IdProveedor { get; set; } = default!;

    public string Proveedor { get; set; } = default!;

    public string TipoPersona { get; set; } = default!;

    public string Direccion1 { get; set; } = default!;
    public string Direccion2 { get; set; } = default!;
    public string Direccion3 { get; set; } = default!;
    public string Direccion4 { get; set; } = default!;

    public string Comprador { get; set; } = default!;

    public string Estado { get; set; } = default!;

    public ContactoProveedorDto Contacto { get; set; } = default!;

    public ImpsDto Imps { get; set; } = default!;
}

public class ContactoProveedorDto
{
    public string Contacto { get; set; } = default!;

    public string Telefono { get; set; } = default!;

    public string CorreoExt { get; set; } = default!;

    public string CorreoInt { get; set; } = default!;
}

public class ImpsDto
{
    public string Ruc { get; set; } = default!;
}
