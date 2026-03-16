using AplicacionMaestro.Domain.Enums;

namespace AplicacionMaestro.Application.Common.Mappings
{
    public static class TipoPersonaMapper
    {
        public static TipoPersona FromString(string tipo)
        {
            return tipo switch
            {
                "1" => TipoPersona.Natural,
                "2" => TipoPersona.Juridica,
                _ => throw new ArgumentException("TipoPersona inválido")
            };
        }

        public static TipoPersona FromEnum(TipoPersona tipoPersona)
        {
            return tipoPersona switch
            {
                TipoPersona.Natural => TipoPersona.Natural,
                TipoPersona.Juridica => TipoPersona.Juridica,
                _ => throw new ArgumentException("TipoPersona inválido")
            };
        }

        public static string ToDatabase(TipoPersona tipo)
            => tipo switch
            {
                TipoPersona.Natural => "1",
                TipoPersona.Juridica => "2",
                _ => throw new ArgumentOutOfRangeException(nameof(tipo))
            };
    }

}
