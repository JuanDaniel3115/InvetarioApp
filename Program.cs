using InventarioApp.Factories;
using InventarioApp.Repositories;
using InventarioApp.Models;
using InventarioApp.Infrastructure;

Console.WriteLine("===== Prueba Integracion JOSN  =======");

var productos = new List<Producto>
{
    ProductoFactory.Crear("Laptop", 1200.00m, 3, CategoriaProducto.Electronica),
    ProductoFactory.Crear("Camisa", 45.00m, 15, CategoriaProducto.Ropa),
    ProductoFactory.Crear("Arroz", 12.00m, 50, CategoriaProducto.Alimentos),
    ProductoFactory.Crear("Lampara", 35.00m, 2, CategoriaProducto.Hogar),
    ProductoFactory.Crear("Balon", 25.00m, 8, CategoriaProducto.Deportes),
    ProductoFactory.Crear("Mesa", 150.00m, 4, CategoriaProducto.Muebles)
};

var generador = new GeneradorReportes(productos);

Console.WriteLine(generador.GenerarResumen());
Console.WriteLine();

Console.WriteLine(generador.GenerarReporteStockBajo());
Console.WriteLine();

Console.WriteLine(generador.GenerarTopProductos());
Console.WriteLine();

Console.WriteLine(generador.ExportarCsv());
Console.WriteLine();

Console.WriteLine(generador.ExportarResumenJson());
Console.WriteLine();