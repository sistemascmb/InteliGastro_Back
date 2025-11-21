using System;

namespace WebApi.Application.DTO.Agenda
{
    public class AgendaSearchResultDto
    {
        public long medicalscheduleid { get; set; }
        public int PacientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int StudiesId { get; set; }
        public int Status { get; set; }

        // Datos del paciente
        public string DocumentNumber { get; set; }
        public string Names { get; set; }
        public string LastNames { get; set; }
    }
}