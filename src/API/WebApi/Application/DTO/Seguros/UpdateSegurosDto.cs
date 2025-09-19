namespace WebApi.Application.DTO.Seguros
{
    public class UpdateSegurosDto
    {
        public long insuranceid { get; set; }
        public string Name { get; set; }
        public string Identification { get; set; }
        public string Details { get; set; }
        public string Adress { get; set; }
        public Boolean Status { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
