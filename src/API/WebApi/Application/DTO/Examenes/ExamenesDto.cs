namespace WebApi.Application.DTO.Examenes
{
    public class ExamenesDto
    {
        public long examsid { get; set; }
        public string Description { get; set; }
        public string Abbreviation { get; set; }
        public Boolean Status { get; set; }
        public int Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
