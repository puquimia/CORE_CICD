using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CICD.LIB.Entidad.Tabla;
using CICD.LIB.Datos;
using Dapper.Contrib.Extensions;
using System.Transactions;

namespace CICD.LIB.Logica
{
    public class GClientes 
    {
        #region Consultas
        public List<Cliente> TraerClientes()
		{
			using (var connection = Conexion.ConnectionFactory())
			{
				return connection.GetAll<Cliente>().ToList();
			}
		}

		public Cliente TraerClienteEntidad(int idCliente)
		{
			using (var connection = Conexion.ConnectionFactory())
			{
				return connection.Get<Cliente>(idCliente);
			}
		}
		#endregion

		#region Verificaciones

		#endregion

		#region Transacciones
		public void Registrar(Cliente eCliente)
		{
			using (var connection = Conexion.ConnectionFactory())
			{
				using (TransactionScope ts = new TransactionScope())
				{
					connection.Insert(eCliente);
					ts.Complete();
				}
			}
		}

		public void Modificar(Cliente eCliente)
		{
			using (var connection = Conexion.ConnectionFactory())
			{
				using (TransactionScope ts = new TransactionScope())
				{
					connection.Update(eCliente);
					ts.Complete();
				}
			}
		}
		public void Eliminar(int idCliente)
		{
			using (var connection = Conexion.ConnectionFactory())
			{
				using (TransactionScope ts = new TransactionScope())
				{
					connection.Delete(new Cliente()
					{
						Id = idCliente
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
