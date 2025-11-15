namespace WebApi.Application.DTO.Personal
{
    public class PersonalDto
    {
        public long personalid { get; set; }
        public int CentroId { get; set; }
        public string Documento { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FecNac { get; set; }
        public string Genero { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public Boolean Estado { get; set; }
        public string Titulo { get; set; }
        public string Grado { get; set; }
        public string NLicencia { get; set; }
        public string TipoTrabajo { get; set; }
        public int Departamento { get; set; }
        public int Provincia { get; set; }
        public int Distrito { get; set; }
        public int Pais { get; set; }
        public int TipoDoc { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public Boolean IsDeleted { get; set; }
        public string Photo { get; set; }
        public string Firma { get; set; }
        public string CabeceraPlantilla { get; set; }

    }
}
