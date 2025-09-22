namespace WebApi.Application.DTO.Suministros
{
    public class CreateSuministrosDto
    {
        //public long provisionid { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Comments { get; set; }
        public decimal Price { get; set; }
        public Boolean Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
