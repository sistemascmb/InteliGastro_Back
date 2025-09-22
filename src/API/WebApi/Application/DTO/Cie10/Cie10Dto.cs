namespace WebApi.Application.DTO.Cie10
{
    public class Cie10Dto
    {
        public long cieid { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int GenderId { get; set; }
        public Boolean Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
