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
    public class GProductos 
    {
        #region Consultas
        public List<Producto> TraerProductos()
		{
			using (var connection = Conexion.ConnectionFactory())
			{
				return connection.GetAll<Producto>().ToList();
			}
		}

		public Producto TraerProductoEntidad(int idProducto)
		{
			using (var connection = Conexion.ConnectionFactory())
			{
				return connection.Get<Producto>(idProducto);
			}
		}
		#endregion

		#region Verificaciones

		#endregion

		#region Transacciones
		public void Registrar(Producto eProducto)
		{
			using (var connection = Conexion.ConnectionFactory())
			{
				using (TransactionScope ts = new TransactionScope())
				{
					connection.Insert(eProducto);
					ts.Complete();
				}
			}
		}

		public void Modificar(Producto eProducto)
		{
			using (var connection = Conexion.ConnectionFactory())
			{
				using (TransactionScope ts = new TransactionScope())
				{
					connection.Update(eProducto);
					ts.Complete();
				}
			}
		}
		public void Eliminar(int idProducto)
		{
			using (var connection = Conexion.ConnectionFactory())
			{
				using (TransactionScope ts = new TransactionScope())
				{
					connection.Delete(new Producto()
					{
						Id = idProducto
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
