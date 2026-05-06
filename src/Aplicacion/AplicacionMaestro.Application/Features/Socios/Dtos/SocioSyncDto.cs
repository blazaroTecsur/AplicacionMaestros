namespace AplicacionMaestro.Application.Features.Socios.Dtos
{
    public class SocioSyncDto
    {
        //public string TipoEvento { get; set; } = null!;
        public int IdSocio { get; set; }
        public string Socio { get; set; } = null!;
        public string TipoEmpleado { get; set; } = null!;
        public string? NroRef { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Supervisor { get; set; }
        public string? CodTrabajo { get; set; }
        public string? TipoPago { get; set; }
        public bool Activo { get; set; }
        public SocioGeneralDto General { get; set; } = null!;
        //public List<AptitudSyncDto> Aptitudes { get; set; } = new();
        //public List<CertificadoSyncDto> Certificaciones { get; set; } = new();
    }
    public class SocioGeneralDto
    {
        public string? Email { get; set; }
        public string? DireccLocaliz { get; set; }
        public string? DireccMensaje { get; set; }
        public string? Almacen { get; set; }
        public string? Departamento { get; set; }
        public string? Usuario { get; set; }
    }
}
