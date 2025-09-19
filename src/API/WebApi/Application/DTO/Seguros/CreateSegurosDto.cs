namespace WebApi.Application.DTO.Seguros
{
    public class CreateSegurosDto
    {
        //public long insuranceid { get; set; }
        public string Name { get; set; }
        public string Identification { get; set; }
        public string Details { get; set; }
        public string Adress { get; set; }
        public Boolean Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
