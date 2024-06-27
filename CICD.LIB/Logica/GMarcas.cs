using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CICD.LIB.Datos;
using CICD.LIB.Entidad.Tabla;
using Dapper.Contrib.Extensions;

namespace CICD.LIB.Logica
{
    public class GMarcas 
    {
        #region Consultas
        public List<Marca> TraerMarcas()
        {
            using (var connection = Conexion.ConnectionFactory())
            {
                return connection.GetAll<Marca>().ToList();
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
