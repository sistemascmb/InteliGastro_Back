namespace WebApi.Application.DTO.Salas
{
    public class CreateSalasDto
    {
        //public long procedureroomId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Boolean Status { get; set; }
        public int Type { get; set; }
        public int CentroId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
       
    }
}
