using Dapper.Contrib.Extensions;
using System;
using System.Data;
using System.Runtime.Serialization;

namespace CICD.LIB.Entidad.Tabla
{
    [Table("GEN.Cliente")]
    public class Cliente
    {
        #region Atributos
        [Key]
        public int Id { get; set; }
        public int? TipoCliente { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string NombreContacto { get; set; }
        public string TelefonoContacto { get; set; }
        public string CorreoContacto { get; set; }
        public int? Estado { get; set; }
        #endregion
    }
}
