namespace WebApi.Application.DTO.Roles
{
    public class RolesDto
    {
        public long profiletypeid { get; set; }
        public string profile_name { get; set; }
        public string description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
