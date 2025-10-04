namespace WebApi.Application.DTO.SystemParameter
{
    public class UpdateSystemParameterDto
    {
        public long parameterid { get; set; }
        public long groupid { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public int ParentParameterId { get; set; }
        public int Sort { get; set; }
        public int Version { get; set; }
        public Boolean Estado { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
