using CICD.LIB.Datos;
using CICD.LIB.Entidad.Tabla;
using CICD.LIB.Logica;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CICD.TEST
{
	[TestClass]
	public class GlientesTest
	{
		public GClientes clientes = null;
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
			clientes = new GClientes();
		}

		[TestMethod]
		[TestCategory("Clientes")]
		public void TraerProductosTest()
		{
			Assert.IsTrue(clientes.TraerClientes().Count() == 2);
		}

		[TestMethod]
		[TestCategory("Clientes")]
		public void RegistrarClienteTest()
		{
			Cliente eCliente = new Cliente()
			{
				TipoCliente = 1,
				Nombre = "Juan Perez",
				Documento = "874316",
				Telefono = "75145665",
				Estado = 1
			};
			clientes.Registrar(eCliente);
			Assert.IsNotNull(clientes.TraerClienteEntidad(eCliente.Id));
		}

		[TestMethod]
		[TestCategory("Clientes")]
		public void ModificarClienteTest()
		{
			int idCliente = 21;
			Cliente eClienteAnterior = clientes.TraerClienteEntidad(idCliente);
			Assert.AreEqual(eClienteAnterior.Nombre , "Monnet SRL");
			Assert.AreEqual(eClienteAnterior.Documento, "784654656");
			Assert.AreEqual(eClienteAnterior.Direccion, "demo@monnet.com");
			Assert.AreEqual(eClienteAnterior.NombreContacto, "Adrian Fuentes");
			Assert.AreEqual(eClienteAnterior.TelefonoContacto, "75624563");

			Cliente eCliente = new Cliente()
			{
				Id = idCliente,
				TipoCliente = 2,
				Nombre = "Juan Perez",
				Documento = "874316",
				Telefono = "75145665",
				NombreContacto = "Juana Pacheco",
				TelefonoContacto = "74589632",
				CorreoContacto = "demo@demo.local",
				Estado = 1
			};
			clientes.Modificar(eCliente);

			Cliente eClienteNuevo = clientes.TraerClienteEntidad(idCliente);
			Assert.AreEqual(eClienteNuevo.Nombre, eCliente.Nombre);
			Assert.AreEqual(eClienteNuevo.Documento, eCliente.Documento);
			Assert.AreEqual(eClienteNuevo.Direccion, eCliente.Direccion);
			Assert.AreEqual(eClienteNuevo.NombreContacto, eCliente.NombreContacto);
			Assert.AreEqual(eClienteNuevo.TelefonoContacto, eCliente.TelefonoContacto);
		}

		[TestMethod]
		[TestCategory("Clientes")]
		public void EliminarClienteTest()
		{
			int idCliente = 21;
			Assert.IsNotNull(clientes.TraerClienteEntidad(idCliente));
			clientes.Eliminar(idCliente);
			Assert.IsNull(clientes.TraerClienteEntidad(idCliente));
		}
	}
}
