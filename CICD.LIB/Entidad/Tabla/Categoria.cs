using System;
using System.Data;
using System.Runtime.Serialization;

namespace CICD.LIB.Entidad.Tabla
{
    public class Categoria
    {
        #region Atributos
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        #endregion
    }
}
