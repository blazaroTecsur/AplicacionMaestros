using AplicacionMaestro.Domain.Exceptions;

namespace AplicacionMaestro.Domain.ValueObject;

public sealed class NombreCompleto
{
    public string Valor { get; }
    public string Nombres { get; }
    public string ApePaterno { get; }
    public string ApeMaterno { get; }

    private NombreCompleto(string valor, string nombres, string apePaterno, string apeMaterno)
    {
        Valor = valor;
        Nombres = nombres;
        ApePaterno = apePaterno;
        ApeMaterno = apeMaterno;
    }

    public static NombreCompleto Crear(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new DomainException("Nombre completo vacío");

        var partes = valor.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (partes.Length < 3)
            throw new DomainException($"Nombre completo inválido: {valor}");

        var apePaterno = partes[^2];
        var apeMaterno = partes[^1];
        var nombres = string.Join(" ", partes.Take(partes.Length - 2));

        return new NombreCompleto(
            valor.Trim(),
            nombres,
            apePaterno,
            apeMaterno
        );
    }
}

