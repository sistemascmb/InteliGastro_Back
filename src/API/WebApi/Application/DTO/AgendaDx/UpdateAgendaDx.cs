namespace WebApi.Application.DTO.AgendaDx
{
    public class UpdateAgendaDx
    {
        public long medicalscheduledxid { get; set; }
        public int Medical_ScheduleId { get; set; }
        public int cie10id { get; set; }
        public string Description { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
