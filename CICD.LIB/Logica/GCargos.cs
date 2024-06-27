using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CICD.LIB.Entidad.Tabla;
using CICD.LIB.Datos;
using Dapper.Contrib.Extensions;

namespace CICD.LIB.Logica
{
    public class GCargos
    {
        #region Consultas
        public List<Cargo> TraerCargos()
        {
			using (var connection = Conexion.ConnectionFactory())
			{
				return connection.GetAll<Cargo>().ToList();
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
