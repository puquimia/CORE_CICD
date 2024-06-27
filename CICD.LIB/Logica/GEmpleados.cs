using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CICD.LIB.Entidad.Tabla;
using System.Transactions;
using CICD.LIB.Datos;
using Dapper.Contrib.Extensions;
using Dapper;
using System.Text;
using CICD.LIB.Entidad.Personalizado;

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

		public List<Empleados> Buscar(string nombreComppleto)
		{
            using (var connection = Conexion.ConnectionFactory())
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"SELECT 
						 e.Id,
						 e.Nombre,
						 e.ApellidoPaterno,
						 e.ApellidoMaterno,
						 e.FechaNacimiento,
						 se.Descripcion DescripcionSexo,
						 e.HaberBasico,
						 c.Nombre NombreCargo
						FROM GEN.Empleado e 
						JOIN GEN.Varios se ON se.Codigo = e.Sexo AND se.Grupo = 'GEN.Empleado.Sexo'
						JOIN GEN.Cargo c ON c.Id = e.IdCargo
						WHERE  ");
                DynamicParameters dPars = new DynamicParameters();
                if (!string.IsNullOrEmpty(nombreComppleto))
                {
                    sb.Append("(e.Nombre LIKE @NombreCompleto OR e.ApellidoPaterno LIKE @NombreCompleto OR e.ApellidoMaterno LIKE @NombreCompleto) AND   ");
					dPars.Add("NombreCompleto", $"%{nombreComppleto}%", System.Data.DbType.AnsiString);
                }
                sb.Length -= 7;
				return connection.Query<Empleados>(sb.ToString(), dPars).ToList();
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
