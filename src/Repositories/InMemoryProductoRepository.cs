using System.Collections.Frozen;
using InventarioApp.Models;

namespace InventarioApp.Repositories
{
    public class InMemoryProductoRepository : IproductoRepository
    {
        private readonly List<Producto> _producto = new();
        private int _proximoId = 1;

        public void Agregar(Producto producto)
        {
            producto.Id = _proximoId++;
            _producto.Add(producto);
        }
        public Producto? ObtenerPorId(int id)
        {
            return _producto.FirstOrDefault(p => p.Id == id);
        }
        public IEnumerable<Producto> ObtenerTodos()
        {
            return _producto.AsReadOnly();
        }
        public bool Atualizar(Producto producto)
        {
            var existente = ObtenerPorId(producto.Id);
            if (existente == null) return false;
            existente.Nombre = producto.Nombre;
            existente.Cantidad = producto.Cantidad;
            existente.Precio = producto.Precio;
            existente.Categoria = producto.Categoria;
            existente.Estado = producto.Estado;

            return true;
        }

        public bool Eliminar(int id)
        {
            var producto = ObtenerPorId(id);
            if (producto == null) return false;
            return _producto.Remove(producto);
        }
        public int Cantidad => _producto.Count;

        // ================= Busquedas con where usando LINQ ===============================
        public IEnumerable<Producto> BuscarPorCategoria(CategoriaProducto categoria)
        {
            return _producto.Where(p => p.Categoria == categoria);
        }

        public IEnumerable<Producto> BuscarPorNombre(string nombre)
        {
            return _producto.Where(p => p.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase));
        }
        public IEnumerable<Producto> BuscarPorRangoPrecio(decimal precioMinimo, decimal precioMaximo)
        {
            return _producto.Where(p => p.Precio >= precioMinimo && p.Precio <= precioMaximo);
        }

        // === select y any =========

        public IEnumerable<string> ObtenerNombres()
        {
            return _producto.Select(p => p.Nombre); // trasforma la respuesta
        }
        public bool HayStockBajo()
        {
            return _producto.Any(p => p.Cantidad < 5); // responde una condicon booleana 
        }

        public IEnumerable<Producto> ObtenerOrdernadosPorPrecio()
        {
            return _producto.OrderBy(p => p.Precio); // de menor a mayor
        }
        public IEnumerable<Producto> ObtenerTopPorPrecio(int cantidad)
        {
            return _producto.OrderByDescending(p => p.Precio).Take(cantidad); // mayor a menor
        }

        // ============ GropuBy y conversion a Dictionary ========

        public IEnumerable<IGrouping<CategoriaProducto, Producto>> AgruparPorCAtegoria()
        {
            return _producto.GroupBy(p => p.Categoria);// cagrupo comparte categoria
        }

        public Dictionary<CategoriaProducto, int> ContarPorCategoria()
        {
            return _producto
            .GroupBy(p => p.Categoria)
            .ToDictionary(g => g.Key, g => g.Count()); // combierte en un diccionario
        }

        // Agregaciones con Sum, Avegarge y MaxBy 

        public decimal ObtenerValorTotalInventario()
        {
            return _producto.Sum(p => p.ValorTotal);
        }

        public decimal ObbtenerPrecioPromedio()
        {
            if(_producto.Count  == 0) return 0;
            return _producto.Average(p => p.Precio);   // Suma y devide por la cantidad
        }
        public Producto? ObtenerProductoMasCaro()
        {
            return _producto.MaxBy(p => p.Precio); // valor maximo
        }

        public Dictionary<CategoriaProducto, decimal> ObtenerValorPorCategoria()
        {
            return _producto
                .GroupBy(p => p.Categoria)
                .ToDictionary(g => g.Key, g => g.Sum(p => p.ValorTotal));
        }


        public IEnumerable<Producto> ObtenerStockBAjo(int umbral = 5)
        {
            return _producto.Where(p => p.Cantidad < umbral);
        }
    }
}