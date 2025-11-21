namespace WebApi.Application.DTO.Agenda
{
    public class AgendaDto
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
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }

        public string AnotacionesAdicionales { get; set; }
        public int TipoProcedimientoId { get; set; }
        public int UrgenteId { get; set; }

        public int? EstudioTeminadoId { get; set; }
        public int? PdfGeneradoId { get; set; }

        public string? EstructuraHtml { get; set; }
        public string? InformePdf { get; set; }

        public int? DictadoGuardado { get; set; }


        public string? Preparacion { get; set; }


    }
}
