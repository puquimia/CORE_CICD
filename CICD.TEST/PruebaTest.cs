using CICD.LIB.Datos;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CICD.TEST
{
	[TestClass]
	public class PruebaTest
	{
		[TestInitialize]
		public void Setup()
		{
			var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
			Conexion.Initialize(configBuilder.Build());
			using (var connection = Conexion.ConnectionFactory())
			{
				connection.Open();
				connection.Execute("CargarDatos", commandType: CommandType.StoredProcedure);
			}
		}
		[TestMethod]
		public void TestMethod()
		{
		}
	}
}