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
	public class GEmpleadosTest
	{
		public GEmpleados empleados = null;
		
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
			empleados = new GEmpleados();
		}
		
		[TestMethod]
		[TestCategory("Empleados")]
		public void TraerEmpleadosTest()
		{
			Assert.IsTrue(empleados.TraerEmpleados().Count() == 3);
		}

		[TestMethod]
		[TestCategory("Empleados")]
		public void TraerEmpleadoEntidadTest()
		{
			int idEmpleado = 61;
			Assert.IsNotNull(empleados.TraerEmpleadoEntidad(idEmpleado));
		}

		[TestMethod]
		[TestCategory("Empleados")]
		public void RegistrarTest()
		{
			GEmpleados empleados = new GEmpleados();
			Empleado eEmpleado = new Empleado()
			{
				Nombre = "Juan",
				ApellidoPaterno = "Siles",
				ApellidoMaterno = "Aguirre",
				FechaNacimiento = new DateTime(1985, 05, 10),
				TipoDocumentoIdentidad = 1,
				DocumentoIdentidad = "545781521",
				Sexo = "M",
				EstadoCivil = 1,
				Telefono = "33214526",
				Correo = "jsiles@gmail.com",
				Direccion = "Los lotes",
				HaberBasico = 3500m,
				IdCargo = 10
			};
			empleados.Registrar(eEmpleado);
			Assert.IsTrue(empleados.TraerEmpleados().Count() == 4);
		}
		
		[TestMethod]
		[TestCategory("Empleados")]
		public void ModificarTest()
		{
			int idEmpleado = 61;
			Empleado eEmpleadoAnterior = empleados.TraerEmpleadoEntidad(idEmpleado);
			Assert.AreEqual(eEmpleadoAnterior.Nombre, "PAOLA");
			Assert.AreEqual(eEmpleadoAnterior.ApellidoPaterno, "MONTAÑO");
			Assert.AreEqual(eEmpleadoAnterior.ApellidoMaterno, "PARDO");
			Assert.AreEqual(eEmpleadoAnterior.FechaNacimiento, new DateTime(1985,05,14));
			Assert.AreEqual(eEmpleadoAnterior.HaberBasico, 1500m);

			Empleado eEmpleado = new Empleado()
			{
				Id = idEmpleado,
				Nombre = "Juan",
				ApellidoPaterno = "Siles",
				ApellidoMaterno = "Aguirre",
				FechaNacimiento = new DateTime(1985, 05, 10),
				TipoDocumentoIdentidad = 1,
				DocumentoIdentidad = "545781521",
				Sexo = "M",
				EstadoCivil = 1,
				Telefono = "33214526",
				Correo = "jsiles@gmail.com",
				Direccion = "Los lotes",
				HaberBasico = 3500m,
				IdCargo = 10
			};

			empleados.Modificar(eEmpleado);
			Empleado eEmpleadoActualizado = empleados.TraerEmpleadoEntidad(idEmpleado);
			Assert.AreEqual(eEmpleadoActualizado.Nombre, eEmpleado.Nombre);
			Assert.AreEqual(eEmpleadoActualizado.ApellidoPaterno, eEmpleado.ApellidoPaterno);
			Assert.AreEqual(eEmpleadoActualizado.ApellidoMaterno, eEmpleado.ApellidoMaterno);
			Assert.AreEqual(eEmpleadoActualizado.FechaNacimiento, eEmpleado.FechaNacimiento);
			Assert.AreEqual(eEmpleadoActualizado.HaberBasico, eEmpleado.HaberBasico);
		}
		
		[TestMethod]
		[TestCategory("Empleados")]
		public void EliminarTest()
		{
			int idEmpleado = 61;
			List<Empleado> lEmpleados = empleados.TraerEmpleados();
			Assert.IsTrue(lEmpleados.Where(x => x.Id == idEmpleado).Count() == 1);

			empleados.Eliminar(61);
			Assert.IsTrue(empleados.TraerEmpleados().Count() == 2);

			lEmpleados = empleados.TraerEmpleados();
			Assert.IsFalse(lEmpleados.Where(x => x.Id == idEmpleado).Count() == 1);
		}
	}
}
