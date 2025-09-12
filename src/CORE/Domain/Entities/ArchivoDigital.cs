using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Domain.Entities
{
    public class ArchivoDigital
    {
        [Dapper.Contrib.Extensions.Key]
        public int IdArchivo { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public int IdUsuario { get; set; }
        public string Equipo { get; set; }
        public int IdProveedor { get; set; }
        public byte[] Archivo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaArchivo { get; set; }
        public string TipoArchivo { get; set; }
        public int IdAtencion { get; set; }
        public string Historia { get; set; }
    }
}
