using AplicacionMaestro.Domain.Enums;
using AplicacionMaestro.Domain.ValueObject;

namespace AplicacionMaestro.Domain.Entities;

public class Proveedor
{
    public string IdExternal { get; private set; }
    public string Ruc { get; private set; }
    public string RazonSocial { get; private set; }

    public TipoPersona TipoPersona { get; private set; }

    public string Direccion1 { get; private set; }
    public string? Direccion2 { get; private set; }
    public string? Direccion3 { get; private set; }
    public string? Direccion4 { get; private set; }

    public string Comprador { get; private set; }

    public ContactoProveedor Contacto { get; private set; }

    public Estado Estado { get; private set; }
    public string UsuarioCreacion { get; private set; }
    public string UsuarioModificacion { get; private set; }
    public DateTime FechaCreacion { get; private set; }
    public DateTime FechaModificacion { get; private set; }

    private Proveedor() { }

    private Proveedor(
        string idExternal,
        string ruc,
        string razonSocial,
        TipoPersona tipoPersona,
        string direccion1,
        string direccion2,
        string direccion3,
        string direccion4,
        string comprador,
        ContactoProveedor contacto,
        Estado estado)
    {
        IdExternal = idExternal;
        Ruc = ruc;
        RazonSocial = razonSocial;
        TipoPersona = tipoPersona;

        Direccion1 = direccion1;
        Direccion2 = direccion2;
        Direccion3 = direccion3;
        Direccion4 = direccion4;

        Comprador = comprador;
        Contacto = contacto;

        Estado = estado;

        FechaCreacion = DateTime.UtcNow;
        FechaModificacion = DateTime.UtcNow;
    }

    public static Proveedor Create(
        string idExternal,
        string ruc,
        string razonSocial,
        TipoPersona tipoPersona,
        string direccion1,
        string direccion2,
        string direccion3,
        string direccion4,
        string comprador,
        ContactoProveedor contacto,
        Estado estado)
    {
        if (string.IsNullOrWhiteSpace(idExternal))
            throw new ArgumentException("IdExternal es obligatorio");

        if (string.IsNullOrWhiteSpace(ruc))
            throw new ArgumentException("RUC es obligatorio");

        if (contacto == null)
            throw new ArgumentException("Contacto es obligatorio");

        return new Proveedor(
            idExternal,
            ruc,
            razonSocial,
            tipoPersona,
            direccion1,
            direccion2,
            direccion3,
            direccion4,
            comprador,
            contacto,
            estado);
    }

    public bool TieneCambios(
        string razonSocial,
        TipoPersona tipoPersona,
        string direccion1,
        string direccion2,
        string direccion3,
        string direccion4,
        string comprador,
        ContactoProveedor contacto,
        Estado estado)
    {
        return RazonSocial != razonSocial ||
               TipoPersona != tipoPersona ||
               Direccion1 != direccion1 ||
               Direccion2 != direccion2 ||
               Direccion3 != direccion3 ||
               Direccion4 != direccion4 ||
               Comprador != comprador ||
               Estado != estado ||
               Contacto.Nombre != contacto.Nombre ||
               Contacto.Telefono != contacto.Telefono ||
               Contacto.CorreoExterno != contacto.CorreoExterno ||
               Contacto.CorreoInterno != contacto.CorreoInterno;
    }


    public void Actualizar(
        string razonSocial,
        TipoPersona tipoPersona,
        string direccion1,
        string direccion2,
        string direccion3,
        string direccion4,
        string comprador,
        ContactoProveedor contacto,
        Estado estado)
    {
        RazonSocial = razonSocial;
        TipoPersona = tipoPersona;

        Direccion1 = direccion1;
        Direccion2 = direccion2;
        Direccion3 = direccion3;
        Direccion4 = direccion4;

        Comprador = comprador;
        Contacto = contacto;
        Estado = estado;

        UsuarioModificacion = "SyncProveedoresHandler"; // Aquí podrías usar un servicio de usuario para obtener el usuario actual
        FechaModificacion = DateTime.UtcNow;
    }
}

