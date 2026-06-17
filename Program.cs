// ============================================================

// SISTEMA DE INVENTARIO - Clase 1.1

// Estado: Mensaje de bienvenida

// ============================================================

using System.Reflection; // Importar el espacio de nombres para trabajar con ensamblados

var assembly = Assembly.GetExecutingAssembly(); // Obtener información del ensamblado actual

var version = assembly.GetName().Version; // Obtener la versión del ensamblado

Console.WriteLine("==========================================");

Console.WriteLine("    SISTEMA DE GESTIÓN DE INVENTARIO      ");

Console.WriteLine("==========================================");

Console.WriteLine();

Console.WriteLine($"Versión del sistema: {version}");

Console.WriteLine($"Plataforma: {Environment.OSVersion}");

Console.WriteLine($".NET Version: {Environment.Version}");

Console.WriteLine();

Console.WriteLine("Estructura del proyecto:");
Console.WriteLine("  InventarioApp/"); 
Console.WriteLine("  ├── program.cs"); 
Console.WriteLine("  ├── InventarioApp.csproj"); 
Console.WriteLine("  ├── gitignore"); 
Console.WriteLine("  ├── README.md"); 
Console.WriteLine("  ├── src/"); 
Console.WriteLine("      ├── Models/"); 
Console.WriteLine("Configuracion .csproject: Define el proyecto y sus dependencias.");

Console.WriteLine("Metadatos configurados");

Console.WriteLine();