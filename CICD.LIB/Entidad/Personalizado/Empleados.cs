using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICD.LIB.Entidad.Personalizado
{
    public class Empleados
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string DescripcionSexo { get; set; }
        public decimal? HaberBasico { get; set; }
        public string NombreCargo { get; set; }
    }
}
