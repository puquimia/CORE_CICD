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
	public class GProductosTest
	{
		public GProductos productos = null;
		
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
			productos = new GProductos();
		}
		
		[TestMethod]
		[TestCategory("Productos")]
		public void TraerProductosTest()
		{
			Assert.IsTrue(productos.TraerProductos().Count() == 4);
		}

		[TestMethod]
		[TestCategory("Productos")]
		public void RegistrarProductoTest()
		{
			Producto eProducto = new Producto()
			{
				Codigo = "4545789",
				Nombre = "Demo producto",
				PrecioVenta = 450m,
				StockMinimo = 10,
				IdUnidadMedida = 300,
				IdCategoria = 100,
				IdMarca = 200,
				Estado = 1
			};
			productos.Registrar(eProducto);
			Assert.IsNotNull(productos.TraerProductoEntidad(eProducto.Id));
		}

		[TestMethod]
		[TestCategory("Productos")]
		public void ModificarProductoTest()
		{
			int idProducto = 4;
			Producto eProductoAnterior = productos.TraerProductoEntidad(idProducto);
			Assert.AreEqual(eProductoAnterior.Nombre, "Microsoft Windows 10 Pro");
			Assert.AreEqual(eProductoAnterior.PrecioVenta, 50m);
			Assert.AreEqual(eProductoAnterior.StockMinimo, 5);

			Producto eProducto = new Producto()
			{
				Id = idProducto,
				Codigo = "4545789",
				Nombre = "Demo producto",
				PrecioVenta = 450m,
				StockMinimo = 10,
				IdUnidadMedida = 300,
				IdCategoria = 100,
				IdMarca = 200,
				Estado = 1
			};
			productos.Modificar(eProducto);
			Producto eProductoActual = productos.TraerProductoEntidad(idProducto);
			Assert.AreEqual(eProductoActual.Nombre, eProducto.Nombre);
			Assert.AreEqual(eProductoActual.PrecioVenta, eProducto.PrecioVenta);
			Assert.AreEqual(eProductoActual.StockMinimo, eProducto.StockMinimo);
		}

		[TestMethod]
		[TestCategory("Productos")]
		public void EliminarProductoTest()
		{
			int idProducto = 1;
			Assert.IsNotNull(productos.TraerProductoEntidad(idProducto));

			productos.Eliminar(idProducto);
			Assert.IsNull(productos.TraerProductoEntidad(idProducto));
		}
	}
}
