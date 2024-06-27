using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CICD.LIB.Entidad.Tabla;
using CICD.LIB.Datos;
using Dapper.Contrib.Extensions;
using System.Transactions;
using CICD.LIB.Entidad.Personalizado;
using Dapper;
using System.Text;

namespace CICD.LIB.Logica
{
    public class GClientes 
    {
        #region Consultas
        public List<Clientes> Buscar(string nombreCliente)
        {
			StringBuilder sb= new StringBuilder();
            sb.Append(@"SELECT 
								 c.Id,
								 vt.Descripcion DescripcionTipo,
								 c.Nombre,
								 c.Documento,
								 c.Telefono,
								 c.Correo,
								 c.NombreContacto,
								 ve.Descripcion DescripcionEstado
								FROM GEN.Cliente c 
								JOIN GEN.Varios vt ON vt.Codigo = c.TipoCliente AND vt.Grupo = 'GEN.Cliente.TipoCliente'
								JOIN GEN.Varios ve ON ve.Codigo = c.Estado AND ve.Grupo = 'GEN.Cliente.Estado'
								WHERE  ");
            DynamicParameters dParameters = new DynamicParameters();
            if (!string.IsNullOrEmpty(nombreCliente) )
			{
				sb.Append($"c.Nombre LIKE @NombreCliente AND   ");
				dParameters.Add("NombreCliente", $"%{nombreCliente}%", System.Data.DbType.AnsiString);
			}
			sb.Length -= 7;
            using (var connection = Conexion.ConnectionFactory())
            {
                return connection.Query<Clientes>(sb.ToString(), dParameters).ToList();
            }
        }
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
		public bool ExisteNombreClienteJuridico(string nombreCliente, int idClienteIgnorar)
		{
            using (var connection = Conexion.ConnectionFactory())
            {
				DynamicParameters dParameters = new DynamicParameters();
				dParameters.Add("NombreCliente", nombreCliente, System.Data.DbType.AnsiString);
                return connection.Query($"SELECT Id FROM GEN.Cliente WHERE Nombre = @NombreCliente AND TipoCliente = 2 AND Id <> {idClienteIgnorar}", dParameters).Count() > 0;
            }
        }
        public bool ExisteDocumentoClienteJuridico(string documentoCliente, int idClienteIgnorar)
        {
            using (var connection = Conexion.ConnectionFactory())
            {
                DynamicParameters dParameters = new DynamicParameters();
                dParameters.Add("DocumentoCliente", documentoCliente, System.Data.DbType.AnsiString);
                return connection.Query($"SELECT Id FROM GEN.Cliente WHERE Documento = @DocumentoCliente AND TipoCliente = 2 AND Id <> {idClienteIgnorar}", dParameters).Count() > 0;
            }
        }
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
