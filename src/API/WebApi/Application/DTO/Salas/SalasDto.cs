namespace WebApi.Application.DTO.Salas
{
    public class SalasDto
    {
        public long procedureroomid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Boolean Status { get; set; }
        public int Type { get; set; }
        public int CentroId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
