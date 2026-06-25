using InventarioApp.Factories;
using InventarioApp.Repositories;
using InventarioApp.Models;
using InventarioApp.Infrastructure;

Console.WriteLine("===== InventarioApp =======");

var fileManager = new Filemanager();
string contenido = "Inventario Actualizado";
fileManager.Escribir("inventario.txt",contenido);

string leerContenido = fileManager.Leer("inventario.txt");
Console.WriteLine(contenido);

var repository = new InMemoryProductoRepository();
var laptop = ProductoFactory.Crear(nombre: "Laptop Dell XPS 13", precio: 1200, cantidad: 5, CategoriaProducto.Electronica);
var mouse = ProductoFactory.Crear(nombre: "Mouse Logitech MX Master", precio: 99, cantidad: 20, CategoriaProducto.Electronica);
var teclado = ProductoFactory.Crear(nombre: "Teclado Mecánico", precio: 150, cantidad: 3, CategoriaProducto.Electronica);
var silla = ProductoFactory.Crear(nombre: "Silla Ergonómica Herman Miller", precio: 500, cantidad: 8, CategoriaProducto.Muebles);
var escritorio = ProductoFactory.Crear(nombre: "Escritorio Stand-up", precio: 300, cantidad: 2, CategoriaProducto.Muebles);

repository.Agregar(laptop);
repository.Agregar(mouse);
repository.Agregar(teclado);
repository.Agregar(silla);
repository.Agregar(escritorio);

// Console.WriteLine($"Productos Agregados: {repository.Cantidad}\n");

//Consultas Basicas LINQ
var electronicos = repository.BuscarPorCategoria(CategoriaProducto.Electronica);

// Console.WriteLine($"Prodcutos de electronica: ");

foreach(var producto in electronicos)
{
    // Console.WriteLine($"{producto.Nombre} : ${producto.Precio}");
}
var conMouse = repository.BuscarPorNombre("mouse");
// Console.WriteLine($"\nProductos con Mouse en el nombre:");
foreach(var producto in conMouse)
{
    // Console.WriteLine($"{producto.Nombre} ");

}

var nombres = repository.ObtenerNombres();
// Console.WriteLine($"\nTodos los nombre {string.Join(",",nombres)}");

var hayStockBajo = repository.HayStockBajo();
// Console.WriteLine($"\n Hay stock bajo ? {hayStockBajo}");