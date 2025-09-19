namespace WebApi.Application.DTO.Roles
{
    public class UpdateRolesDto
    {
        public long profiletypeid { get; set; }
        public string profile_name { get; set; }
        public string description { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
