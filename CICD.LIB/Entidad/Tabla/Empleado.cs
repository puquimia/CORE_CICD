using Dapper.Contrib.Extensions;
using System;
using System.Data;
using System.Runtime.Serialization;

namespace CICD.LIB.Entidad.Tabla
{
    [Table("GEN.Empleado")]
    public class Empleado
    {
        #region Atributos
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? TipoDocumentoIdentidad { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string Sexo { get; set; }
        public int? EstadoCivil { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public decimal? HaberBasico { get; set; }
        public int? IdCargo { get; set; }
        #endregion
    }
}
