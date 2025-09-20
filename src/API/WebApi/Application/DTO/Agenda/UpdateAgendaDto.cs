namespace WebApi.Application.DTO.Agenda
{
    public class UpdateAgendaDto
    {
        public long medicalscheduleid { get; set; }
        public int PacientId { get; set; }
        public int CentroId { get; set; }
        public int PersonalId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string HoursMedicalShedule { get; set; }
        public int TypeofAppointment { get; set; }
        public int OriginId { get; set; }
        public string OtherOrigins { get; set; }
        public int InsuranceId { get; set; }
        public int LetterOfGuarantee { get; set; }
        public int Status { get; set; }
        //tipo si es consulta o procedimiento
        public int TypeOfAttention { get; set; }
        // procedimientos
        public int TypeOfPatient { get; set; }
        public int Referral_doctorsId { get; set; }
        public int CenterOfOriginId { get; set; }
        public string AnotherCenter { get; set; }
        public int ProcedureRoomId { get; set; }
        public int ResourcesId { get; set; }
        public int StudiesId { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
