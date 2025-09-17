namespace WebApi.Application.DTO.Recursos
{
    public class RecursosDto
    {
        public long resourcesid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public int CentroId { get; set; }
        public Boolean Status { get; set; }
        public int procedureroomid { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
