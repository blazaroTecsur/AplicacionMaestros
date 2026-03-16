namespace AplicacionMaestro.Domain.Entities;

public class Almacen
{
    public string IdAlmacenExternal { get; private set; }

    public string Codigo { get; private set; } = null!;

    public string Nombre { get; private set; } = null!;

    public string Direccion1 { get; private set; } = null!;

    public string? Direccion2 { get; private set; } = null!;

    public string? Direccion3 { get; private set; } = null!;

    public string? Direccion4 { get; private set; } = null!;

    public string Ciudad { get; private set; } = null!;

    public string CodProvincia { get; private set; } = null!;

    public string CodigoPostal { get; private set; } = null!;

    public string Contacto { get; private set; } = null!;

    public string Telefono { get; private set; } = null!;

    public string? Fax { get; private set; } = null!;

    public string VatId { get; private set; } = null!;

    private Almacen() { }

    public Almacen(
        string idAlmacenExternal,
        string codigo,
        string nombre,
        string direccion1,
        string? direccion2,
        string? direccion3,
        string? direccion4,
        string ciudad,
        string codProvincia,
        string codigoPostal,
        string contacto,
        string telefono,
        string? fax,
        string vatId)
    {
        IdAlmacenExternal = idAlmacenExternal;
        Codigo = codigo;
        Nombre = nombre;
        Direccion1 = direccion1;
        Direccion2 = direccion2;
        Direccion3 = direccion3;
        Direccion4 = direccion4;
        Ciudad = ciudad;
        CodProvincia = codProvincia;
        CodigoPostal = codigoPostal;
        Contacto = contacto;
        Telefono = telefono;
        Fax = fax;
        VatId = vatId;
    }
    public static Almacen Create(
        string idAlmacenExternal,
        string codigo,
        string nombre,
        string direccion1,
        string? direccion2,
        string? direccion3,
        string? direccion4,
        string ciudad,
        string codProvincia,
        string codigoPostal,
        string contacto,
        string telefono,
        string? fax,
        string vatId)
    {
        if (string.IsNullOrWhiteSpace(idAlmacenExternal))
            throw new ArgumentException("Id del almacen es obligatorio");

        if (string.IsNullOrWhiteSpace(codigo))
            throw new ArgumentException("El código del almacen es obligatorio");

        return new Almacen(
            idAlmacenExternal,
            codigo,
            nombre,
            direccion1,
            direccion2,
            direccion3,
            direccion4,
            ciudad,
            codProvincia,
            codigoPostal,
            contacto,
            telefono,
            fax,
            vatId);
    }
}