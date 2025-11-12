namespace WebApi.Application.DTO.ArchivoDigital
{
    public class CreateArchivoDigitalDto
    {
        //public long digitalfileid { get; set; }
        public DateTime Date { get; set; }
        public string Hour { get; set; }
        public string Desktop { get; set; }
        public byte[] Archive { get; set; }
        public string Description { get; set; }
        public string TypeArchive { get; set; }
        public int Medical_ScheduleId { get; set; }
        public Boolean Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        
    }
}
