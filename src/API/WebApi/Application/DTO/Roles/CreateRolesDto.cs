namespace WebApi.Application.DTO.Roles
{
    public class CreateRolesDto
    {
        //public long profiletypeId { get; set; }
        public string profile_name { get; set; }
        public string description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
