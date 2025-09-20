namespace WebApi.Application.DTO.Macros
{
    public class CreateMacrosDto
    {
        //public long macrosid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? PersonalId { get; set; }
        public string Macro { get; set; }
        public Boolean SelectAll { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
