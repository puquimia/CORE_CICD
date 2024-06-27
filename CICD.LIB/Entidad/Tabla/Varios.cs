using Dapper.Contrib.Extensions;
using System;
using System.Data;
using System.Runtime.Serialization;

namespace CICD.LIB.ENT
{
    [Table("GEN.Varios")]
    public class Varios
    {
        #region Atributos
        [ExplicitKey]
        public string Grupo { get; set; }
        [ExplicitKey]
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        #endregion
    }
}
