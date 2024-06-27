using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICD.LIB.Entidad.Personalizado
{
    public class Productos
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int StockMinimo { get; set; }
        public string NombreMarca { get; set; }
        public string NombreCategoria { get; set; }
        public string DescripcionEstado { get; set; }
    }
}
