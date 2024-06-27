using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CICD.LIB.Datos;
using CICD.LIB.Entidad.Tabla;
using Dapper.Contrib.Extensions;

namespace CICD.LIB.Logica
{
    public class GCategorias 
    {
        #region Consultas
        public List<Categoria> TraerCategorias()
        {
            using (var connection = Conexion.ConnectionFactory())
            {
                return connection.GetAll<Categoria>().ToList();
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
