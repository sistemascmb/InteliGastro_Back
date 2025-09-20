namespace WebApi.Application.DTO.Paciente
{
    public class CreatePacienteDto
    {
        //public long pacientid { get; set; }
        public int TypeDoc { get; set; }
        public string DocumentNumber { get; set; }
        public string Names { get; set; }
        public string LastNames { get; set; }
        public DateTime Birthdate { get; set; }
        public int Gender { get; set; }
        public int StatusMarital { get; set; }
        public string Nationality { get; set; }
        public int CentroId { get; set; }
        public string Address { get; set; }
        public int Pais { get; set; }
        public int Department { get; set; }
        public int Province { get; set; }
        public int District { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Boolean Status { get; set; }
        public string MedicalHistory { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
