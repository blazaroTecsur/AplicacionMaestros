using AplicacionMaestro.Application.Features.Empleados.Dtos;
using AplicacionMaestro.Domain.Entities;
using AplicacionMaestro.Domain.Models;
using AutoMapper;

namespace AplicacionMaestro.Application.Mapping;

public class EmpleadoProfile : Profile
{
    public EmpleadoProfile()
    {
        CreateMap<EmpleadoSyncDto, Empleado>()
            .ConstructUsing(dto => Empleado.Crear(
                dto.IdEmpleado,
                dto.Codigo,
                dto.Empleado,
                MapPrincipal(dto.Principal),
                MapNameTax(dto.NameTax),
                MapContacto(dto.Contacto),
                MapRh(dto.Rh)
            ))
            .ForAllMembers(x => x.Ignore());
    }

    private static PrincipalInfo MapPrincipal(PrincipalDto dto)
    {
        //if (dto == null)
        //{
        //    return new PrincipalInfo(
        //        string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
        //        string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
        //        string.Empty, string.Empty, string.Empty
        //    );

        //}
        return new PrincipalInfo(
            dto.Apellido,
            dto.Nombre,
            dto.Alias,
            dto.Cargo,
            dto.Dpto,
            dto.Estado,
            dto.Turno,
            dto.Categoria,
            dto.IdUsuario,
            dto.FrecPago,
            dto.TipEmp,
            dto.GenNomina,
            dto.CtaSueldo
        );
    }

    private static NameTaxInfo MapNameTax(NameTaxDto dto)
    {
        //if (dto == null)
        //{
        //    return new NameTaxInfo(string.Empty, string.Empty, string.Empty, string.Empty);
        //}

        return new NameTaxInfo(
            dto.PrNombre,
            dto.SgNombre,
            dto.PrApellido,
            dto.SgApellido
        );
    }

    private static ContactoInfo MapContacto(ContactoEmpleadoDto dto)
    {
        //if (dto == null)
        //{
        //    return new ContactoInfo(
        //        string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
        //        string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
        //        string.Empty, string.Empty, string.Empty
        //    );
        //}

        return new ContactoInfo(
            dto.Direccion1,
            dto.Direccion2,
            dto.Direccion3,
            dto.Direccion4,
            dto.Ciudad,
            dto.CodProvincia,
            dto.CP,
            dto.Municipio,
            dto.Telefono,
            dto.TelComercial,
            dto.ExtensionTel,
            dto.CorreoElect,
            dto.Correo
        );
    }

    private static RecursosHumanosInfo MapRh(RecursosHumanosDto dto)
    {
        //if (dto == null)
        //{
        //     return new RecursosHumanosInfo(null, null, null);
        //}

        return new RecursosHumanosInfo(
            ParseFecha(dto.FechContr),
            ParseFecha(dto.FechRevis),
            ParseFecha(dto.FechRescis)
        );
    }

    private static IEnumerable<Certificado> MapCertificaciones(List<CertificacionDto> dtos)
    {
        if (dtos == null || !dtos.Any())
            return new List<Certificado>();

        return dtos.Select(c => new Certificado(
            c.Codigo,
            c.Descripcion
        ));
    }

    private static IEnumerable<Aptitud> MapAptitudes(List<AptitudDto> dtos)
    {
        if (dtos == null || !dtos.Any())
            return new List<Aptitud>();

        return dtos.Select(a => new Aptitud(
            a.Codigo,
            a.Descripcion
        ));
    }

    private static DateTime? ParseFecha(string fecha)
    {
        if (string.IsNullOrWhiteSpace(fecha))
            return null;

        if (DateTime.TryParse(fecha, out var result))
            return result;

        return null;
    }
}