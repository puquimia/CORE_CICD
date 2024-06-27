using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CICD.LIB.Entidad.Tabla;
using CICD.LIB.Datos;
using Dapper.Contrib.Extensions;
using System.Transactions;
using Dapper;
using CICD.LIB.Entidad.Personalizado;
using System.Text;

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

		public List<Productos>Buscar(string nombreProducto)
		{
			StringBuilder sb= new StringBuilder();
			sb.Append(@"SELECT 
						 p.Id,
						 p.Codigo,
						 p.Nombre,
						 p.StockMinimo,
						 m.Nombre NombreMarca,
						 ca.Nombre NombreCategoria,
						 ve.Descripcion DescripcionEstado
						FROM GEN.Producto p 
						JOIN GEN.Marca m ON m.Id = p.IdMarca
						JOIN GEN.Categoria ca ON ca.Id = p.IdCategoria
						JOIN GEN.Varios ve ON ve.Codigo = p.Estado AND ve.Grupo = 'GEN.PRODUCTO.ESTADO'
						WHERE  ");
			DynamicParameters dParameters = new DynamicParameters();
			if (!string.IsNullOrEmpty(nombreProducto))
			{
				sb.Append($"(p.Codigo LIKE @NombreProducto OR p.Nombre LIKE @NombreProducto) AND   ");
				dParameters.Add("NombreProducto", $"%{nombreProducto}%", System.Data.DbType.AnsiString);

            }
			sb.Length -= 7;
            using (var connection = Conexion.ConnectionFactory())
            {
                return connection.Query<Productos>(sb.ToString(), dParameters).ToList();
            }
        }
        #endregion

        #region Verificaciones
        public bool ExisteCodigoProducto(string CodigoProducto, int idProductoIgnorar)
        {
            using (var connection = Conexion.ConnectionFactory())
            {
                DynamicParameters dParameters = new DynamicParameters();
                dParameters.Add("CodigoProducto", CodigoProducto, System.Data.DbType.AnsiString);
                return connection.Query($"SELECT Id, Nombre FROM GEN.Producto WHERE Codigo = @CodigoProducto AND Id <> {idProductoIgnorar}", dParameters).Count() > 0;
            }
        }
        public bool ExisteNombreProducto(string nombreProducto, int idProductoIgnorar)
        {
            using (var connection = Conexion.ConnectionFactory())
            {
				DynamicParameters dParameters = new DynamicParameters();
				dParameters.Add("NombreProducto", nombreProducto, System.Data.DbType.AnsiString);
                return connection.Query($"SELECT Id, Nombre FROM GEN.Producto WHERE Nombre = @NombreProducto AND Id <> {idProductoIgnorar}", dParameters).Count() > 0;
            }
        }
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
