namespace WebApi.Application.DTO.Plantilla
{
    public class UpdatePlantillaDto
    {
        public long templatesid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? PersonalId { get; set; }
        public int ExamsId { get; set; }
        public string Plantilla { get; set; }
        public bool Status { get; set; }
        public bool AllPersonalMed { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
