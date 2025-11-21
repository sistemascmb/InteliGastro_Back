using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    [Table("medical_schedule")]
    public class AgendaEntity
    {
        [Dapper.Contrib.Extensions.Key]
        public long medicalscheduleid { get; set; }
        [Column]
        public int PacientId { get; set; }
        [Column]
        public int CentroId { get; set; }
        [Column]
        public int PersonalId { get; set; }
        [Column]
        public DateTime AppointmentDate { get; set; }
        [Column]
        public string HoursMedicalShedule { get; set; }
        [Column]
        public int TypeofAppointment { get; set; }
        [Column]
        public int OriginId { get; set; }
        [Column]
        public string OtherOrigins { get; set; }
        [Column]
        public int InsuranceId { get; set; }
        [Column]
        public int LetterOfGuarantee { get; set; }
        [Column]
        public int Status { get; set; }
        //tipo si es consulta o procedimiento
        [Column]
        public int TypeOfAttention { get; set; }
        // procedimientos
        [Column]
        public int TypeOfPatient { get; set; }
        [Column]
        public int Referral_doctorsId { get; set; }
        [Column]
        public int CenterOfOriginId { get; set; }
        [Column]
        public string AnotherCenter { get; set; }
        [Column]
        public int ProcedureRoomId { get; set; }
        [Column]
        public int ResourcesId { get; set; }
        [Column]
        public int StudiesId { get; set; }
        [Column]
        public DateTime CreatedAt { get; set; }
        [Column]
        public string CreatedBy { get; set; }
        [Column]
        public DateTime UpdatedAt { get; set; }
        [Column]
        public string? UpdatedBy { get; set; }
        [Column]
        public Boolean IsDeleted { get; set; }
        [Column]
        public string AnotacionesAdicionales { get; set; }
        [Column]
        public int TipoProcedimientoId { get; set; }
        [Column]
        public int UrgenteId { get; set; }
        [Column]
        public int? EstudioTeminadoId { get; set; }
        [Column]
        public int? PdfGeneradoId { get; set; }
        [Column]
        public string? EstructuraHtml { get; set; }
        [Column]
        public string? InformePdf { get; set; }
        [Column]
        public int? DictadoGuardado { get; set; }

        public string? Preparacion { get; set; }


    }
}
