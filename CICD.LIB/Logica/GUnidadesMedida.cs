using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CICD.LIB.Datos;
using CICD.LIB.Entidad.Tabla;
using Dapper.Contrib.Extensions;

namespace CICD.LIB.Logica
{
    public class GUnidadesMedida 
    {
        #region Consultas
        public List<UnidadMedida> TraerUnidadesMedida()
        {
            using (var connection = Conexion.ConnectionFactory())
            {
                return connection.GetAll<UnidadMedida>().ToList();
            }
        }
        #endregion

        #region Verificaciones

        #endregion

        #region Transacciones

        #endregion

        #region Procesos

        #endregion
    }
}
