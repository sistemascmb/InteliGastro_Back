namespace WebApi.Application.DTO.Cie10
{
    public class CreateCie10Dto
    {
        //public long cieid { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int GenderId { get; set; }
        public Boolean Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
