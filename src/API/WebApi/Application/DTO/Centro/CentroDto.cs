namespace WebApi.Application.DTO.Centro
{
    public class CentroDto
    {
        //--------------------------------------------------------------
        // Propiedades DTO
        //--------------------------------------------------------------
       
        public long centroid { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Abreviatura { get; set; }
        public string InicioAtencion { get; set; }
        public string FinAtencion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int Departamento { get; set; }
        public int Provincia { get; set; }
        public int Distrito { get; set; }
        public int Pais { get; set; }
        public string RUC { get; set; }
        public Boolean Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
