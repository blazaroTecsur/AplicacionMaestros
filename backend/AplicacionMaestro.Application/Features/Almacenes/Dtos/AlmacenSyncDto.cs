namespace AplicacionMaestro.Application.Features.Almacenes.Dtos
{
    public class AlmacenSyncDto
    {
        public string IdAlmacen { get; set; }
        public string Codigo { get; set; }  = null!;
        public string Almacen { get; set; }  = null!;
        public string Direccion1 { get; set; }  = null!;
        public string? Direccion2 { get; set; }  
        public string? Direccion3 { get; set; }  
        public string? Direccion4 { get; set; }  
        public string Ciudad { get; set; }  = null!;   
        public string CodProvincia { get; set; }  = null!;
        public string CP { get; set; }  = null!;
        public string Contacto { get; set; }  = null!;
        public string Telefono { get; set; }  = null!;
        public string? Fax { get; set; }  
        public IvaUeDto IvaUe { get; set; }  = new();
    }
    public class IvaUeDto
    {
        public string VatId { get; set; }  = null!;
    }
}
