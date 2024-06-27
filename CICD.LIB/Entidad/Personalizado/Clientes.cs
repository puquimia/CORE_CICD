using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICD.LIB.Entidad.Personalizado
{
    public class Clientes
    {
        public int Id { get; set; }
        public string DescripcionTipo { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string NombreContacto { get; set; }
        public string DescripcionEstado { get; set; }
    }
}
