using Dapper.Contrib.Extensions;
using System;
using System.Data;
using System.Runtime.Serialization;

namespace CICD.LIB.Entidad.Tabla
{
    [Table("GEN.UnidadMedida")]
    public class UnidadMedida
    {
        #region Atributos
        [Key]
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Abreviacion { get; set; }
        #endregion
    }
}
