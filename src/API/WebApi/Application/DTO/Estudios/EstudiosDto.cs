namespace WebApi.Application.DTO.Estudios
{
    public class EstudiosDto
    {
        public long studiesid { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }
        public int OperatingHours { get; set; }
        public Boolean Status { get; set; }
        public Boolean InformedConsent { get; set; }
        public int CentroId { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
