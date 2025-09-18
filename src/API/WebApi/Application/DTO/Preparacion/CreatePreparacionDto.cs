namespace WebApi.Application.DTO.Preparacion
{
    public class CreatePreparacionDto
    {
        //public long preparationid { get; set; }
        public string Description { get; set; }
        public Boolean Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
