using System;
using CICD.LIB.ENT;
using CICD.LIB.Datos;
using Dapper;

namespace CICD.LIB.Logica
{
    public class GVarios 
    {
        #region Consultas
        public List<Varios>TraerVariosxGrupo(string grupo)
        {
			using (var connection = Conexion.ConnectionFactory())
			{
				connection.Open();
                DynamicParameters dParametros = new DynamicParameters();
                dParametros.Add("Grupo", grupo, System.Data.DbType.AnsiString);
				return connection.Query<Varios>("SELECT Grupo, Codigo, Descripcion FROM GEN.Varios WHERE Grupo = @Grupo", dParametros).ToList();
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
