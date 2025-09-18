namespace WebApi.Application.DTO.Preparacion
{
    public class UpdatePreparacionDto
    {
        public long preparationid { get; set; }
        public string Description { get; set; }
        public Boolean Status { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
