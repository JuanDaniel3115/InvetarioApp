using InventarioApp.Factories;
using InventarioApp.Repositories;
using InventarioApp.Models;
using InventarioApp.Infrastructure;
using InventarioApp.Services;
using System.ComponentModel.Design;

Console.WriteLine("===== Prueba Integracion JOSN  =======");
var servicio = new InventarioService();
bool activo = true;

while (activo)
{
    MostrarMenu();
    string opcion = Console.ReadLine() ?? "";

    switch (opcion)
    {
        case "1":
            AgregarProducto();
            break;
        case "2":
            ListarProductos();
            break;
        case "3":
            BuscarPorId();
            break;
        case "4":
            EliminarProducto();
            break;
        case "5":
            BuscarPorCategoria();
            break;
        case "6":
            MostrarResumen();
            break;
        case "7":
            MostrarStockBajo();
            break;
        case "8":
            MostrarEstadisticas();
            break;
        case "9":
            ExportarCsv();
            break;
        case "10":
            activo = false;
            Console.WriteLine("\nHasta Luego!");
            break;
        default:
            Console.WriteLine("\nOpcion no valida!");
            break;

    }


}
void MostrarMenu()
{
    Console.WriteLine("\n=== SISTEMA DE INVENTARIO ===");
    Console.WriteLine("1. Agregar prodcuto");
    Console.WriteLine("2. Listar productos");
    Console.WriteLine("3. Buscar por ID");
    Console.WriteLine("4. Eliminar producto");
    Console.WriteLine("5. Buscar por categoria");
    Console.WriteLine("6. Ver resumen");
    Console.WriteLine("7. Ver stock bajo");
    Console.WriteLine("8. Ver estadisticas");
    Console.WriteLine("9. Exportar CSV");
    Console.WriteLine("10. Salir");
    Console.WriteLine("\nSelecciona una opcion");
}

void AgregarProducto()
{
    Console.WriteLine("\nNombre: ");
    string nombre = Console.ReadLine() ?? "";

    Console.WriteLine("Precio: ");
    decimal precio = decimal.Parse(Console.ReadLine() ?? "0");

    Console.WriteLine("Cantidad: ");
    int cantidad = int.Parse(Console.ReadLine() ?? "0");

    Console.WriteLine("\nCategorias: Electronica, Ropa, Alimentos, Hogar, Deportes, Librios, Muebles, Otros");
    Console.WriteLine("Categoria: ");
    string categoriaStr = Console.ReadLine() ?? "Otros";

    if (Enum.TryParse<CategoriaProducto>(categoriaStr, ignoreCase: true, out var categoria))
    {
        servicio.AgregarProducto(nombre, precio, cantidad, categoria);
        Console.WriteLine("\nProdcuto agregado exitosamente.");
    }
    else
    {
        Console.WriteLine("\nCategoriaa no valida.");
    }
}

void ListarProductos()
{
    var productos = servicio.ObtenerTodosLosProductos();
    if (!productos.Any())
    {
        Console.WriteLine("\nNo hay productos");
        return;
    }
    Console.WriteLine("\n=== PRODUCTOS ===");
    foreach (var p in productos)
    {
        Console.WriteLine($"ID: {p.Id} | {p.Nombre} |  Precio: {p.Precio}  |  Cantidad: {p.Cantidad}  |  Total: {p.ValorTotal}  |  {p.Categoria} ");
    }
}

void BuscarPorId()
{
    Console.Write("\nID del prodcto: ");
    int id = int.Parse(Console.ReadLine() ?? "0");

    var producto = servicio.ObtenerProductoPorId(id);
    if (producto != null)
    {
        Console.WriteLine($"\nID: {producto.Id}");
        Console.WriteLine($"Nombre: {producto.Nombre}");
        Console.WriteLine($"Precio: {producto.Precio}");
        Console.WriteLine($"Cantidad: {producto.Cantidad}");
        Console.WriteLine($"Valor Total: {producto.ValorTotal}");
        Console.WriteLine($"Categoria: {producto.Categoria}");

    }
    else
    {
        Console.WriteLine("\nProducto no encontrado.");
    }
}

void EliminarProducto()
{
    Console.Write("\nID del producto a eliminar: ");
    int id = int.Parse(Console.ReadLine() ?? "0");

    var producto = servicio.ObtenerProductoPorId(id);
    if (producto != null)
    {
        servicio.EliminarProdcuto(id);
        Console.WriteLine("\nPorducto eliminado");
    }
    else
    {
        Console.WriteLine("\nPorducto no encontrado");
    }
}
void BuscarPorCategoria()
{
    Console.WriteLine("\nCategorias: Electronica, Ropa, Alimentos, Hogar, Deportes, Librios, Muebles, Otros");
    Console.WriteLine("Categoria: ");
    string categoriaStr = Console.ReadLine() ?? "Otros";
    if (Enum.TryParse<CategoriaProducto>(categoriaStr, ignoreCase: true, out var categoria))
    {
        var productos = servicio.BuscarPorCategoria(categoria);
        if (!productos.Any())
        {
            Console.WriteLine("\nNo hay prodcutos con esta categoria.");
        }
        Console.WriteLine($"\n=== PRODUCTOS EN {categoria} ===");
        foreach (var p in productos)
        {
            Console.WriteLine($"ID: {p.Id} | {p.Nombre} |  Precio: {p.Precio}  |  Cantidad: {p.Cantidad}  ");
        }
    }
    else
    {
        Console.WriteLine("\nCategoria no valida.");
    }

}
void MostrarResumen()
{
    var resumen = servicio.GenerarResumen();
    Console.WriteLine($"\n{resumen}");
}
void MostrarStockBajo()
{
    var reporte = servicio.GenerarReporteStockBajo();
    Console.WriteLine($"\n{reporte}");
}
void MostrarEstadisticas()
{
     Console.WriteLine($"\n=== ESTADISTICAS ===");
     Console.WriteLine($"Valor total del inventario: ${servicio.ObtenerValorTotalInventario()}");
     Console.WriteLine($"Precion promedio: ${servicio.ObtenerPreciooPromedio()}");

     var masCaro = servicio.ObtnerProductoMasCaro();
     if(masCaro != null)
    {
        Console.WriteLine($"Prodcuto mas caro: {masCaro.Nombre} (${masCaro.Precio})");
    }
}

void ExportarCsv()
{
    string csv = servicio.ExportarCsv();
    Console.WriteLine($"\n{csv}");
}


