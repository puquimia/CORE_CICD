using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CICD.LIB.Entidad.Tabla;
using System.Transactions;
using CICD.LIB.Datos;
using Dapper.Contrib.Extensions;
using Dapper;

namespace CICD.LIB.Logica
{
    public class GEmpleados 
    {
        #region Consultas
		public List<Empleado> TraerEmpleados()
		{
			using (var connection = Conexion.ConnectionFactory())
			{
				return connection.GetAll<Empleado>().ToList();
			}
		}
		public Empleado TraerEmpleadoEntidad(int idEmpleado)
		{
			using (var connection = Conexion.ConnectionFactory())
			{
				return connection.Get<Empleado>(idEmpleado);
			}
		}
		#endregion

		#region Verificaciones

		#endregion

		#region Transacciones
		public void Registrar(Empleado eEmpleado)
        {
            using (var connection = Conexion.ConnectionFactory())
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    connection.Insert(eEmpleado);
                    ts.Complete();
                }
            }
        }

		public void Modificar(Empleado eEmpleado)
		{
			using (var connection = Conexion.ConnectionFactory())
			{
				using (TransactionScope ts = new TransactionScope())
				{
					connection.Update(eEmpleado);
					ts.Complete();
				}
			}
		}
		public void Eliminar(int idEmpleado)
		{
			using (var connection = Conexion.ConnectionFactory())
			{
				using (TransactionScope ts = new TransactionScope())
				{
					connection.Delete(new Empleado()
					{
						Id = idEmpleado
					});
					ts.Complete();
				}
			}
		}
		#endregion

		#region Procesos

		#endregion
	}
}
