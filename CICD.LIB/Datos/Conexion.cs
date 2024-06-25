using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICD.LIB.Datos
{
	public static class Conexion
	{
		private static IConfiguration _configuration;

		public static void Initialize(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public static IDbConnection ConnectionFactory()
		{
			string connectionString = _configuration.GetConnectionString("ConnectionString");
			return new SqlConnection(connectionString);
		}
	}
}
