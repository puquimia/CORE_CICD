using Dapper.Contrib.Extensions;
using System;
using System.Data;
using System.Runtime.Serialization;

namespace CICD.LIB.Entidad.Tabla
{
    [Table("GEN.Producto")]
    public class Producto
    {
        #region Atributos
        [Key]
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioVenta { get; set; }
        public int? StockMinimo { get; set; }
        public int? IdUnidadMedida { get; set; }
        public int? IdMarca { get; set; }
        public int? IdCategoria { get; set; }
        public int? Estado { get; set; }
        #endregion
    }
}
