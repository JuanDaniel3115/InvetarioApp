
using System.Security.Cryptography.X509Certificates;
using InventarioApp.Factories;
using InventarioApp.Infrastructure;
using InventarioApp.Models;
using InventarioApp.Repositories;

namespace InventarioApp.Services
{
    public class InventarioService
    {
        private readonly InMemoryProductoRepository _repository;
        private readonly JsonInentarioStorage _storage;
        private readonly string _rutaInventario;

        public InventarioService(string ruta = "inventario.json")
        {
            _repository = new InMemoryProductoRepository();
            _storage = new JsonInentarioStorage();
            _rutaInventario = ruta;

            CargarInventario();
        }
        private void CargarInventario()
        {
            if (File.Exists(_rutaInventario))
            {
                var productos = _storage.Cargar(_rutaInventario);
                foreach (var producto in productos)
                {
                    _repository.Agregar(producto);
                }
            }
        }
        public void AgregarProducto(string nombre, decimal precio, int cantidad, CategoriaProducto categoria)
        {
            var producto = ProductoFactory.Crear(nombre, precio, cantidad, categoria);
            _repository.Agregar(producto);
            _persistir();
        }
        public IEnumerable<Producto> ObtenerTodosLosProductos()
        {
            return _repository.ObtenerTodos();
        }

        public Producto? ObtenerProductoPorId(int id)
        {
            return _repository.ObtenerPorId(id);
        }
        public void ActualizarProducto(int id, string nombre, decimal precio, int cantida, CategoriaProducto categoria)
        {
            var producto = _repository.ObtenerPorId(id);
            if (producto != null)
            {
                producto.Nombre = nombre;
                producto.Precio = precio;
                producto.Cantidad = cantida;
                producto.Categoria = categoria;
                _repository.Actualizar(producto);
                _persistir();
            }
        }
        public void EliminarProdcuto(int id)
        {
            _repository.Eliminar(id);
            _persistir();
        }

        public IEnumerable<Producto> BuscarPorCategoria(CategoriaProducto categoria)
        {
            return _repository.BuscarPorCategoria(categoria);
        }
        public IEnumerable<Producto> BuscarPorNombre(string nombre)
        {
            return _repository.BuscarPorNombre(nombre);
        }
        public IEnumerable<Producto> ObtenerProductoBajoStock(int minimo = 5)
        {
            return _repository.ObtenerStockBAjo(minimo);
        }
        public decimal ObtenerValorTotalInventario()
        {
            return _repository.ObtenerValorTotalInventario();
        }
        public decimal ObtenerPreciooPromedio()
        {
            return _repository.ObbtenerPrecioPromedio();
        }
        public Producto? ObtnerProductoMasCaro()
        {
            return _repository.ObtenerProductoMasCaro();
        }

        //Metodos de reportes
        public string GenerarResumen()
        {
            var productos = _repository.ObtenerTodos();
            var generar = new GeneradorReportes(productos);
            return generar.GenerarResumen();
        }
        public string GenerarReporteStockBajo(int minimo = 5)
        {
            var productos = _repository.ObtenerTodos();
            var generardor = new GeneradorReportes(productos);
            return generardor.GenerarReporteStockBajo(minimo);
        }
        public string GenerarTopProductos(int cantidad = 5)
        {
            var productos = _repository.ObtenerTodos();
            var generardor = new GeneradorReportes(productos);
            return generardor.GenerarTopProductos(cantidad);
        }
        public string ExportarCsv()
        {
            var productos = _repository.ObtenerTodos();
            var generardor = new GeneradorReportes(productos);
            return generardor.ExportarCsv();
        }

        private void _persistir()
        {
            _storage.CrearBackup(_rutaInventario);
            var productos = _repository.ObtenerTodos().ToList();
            _storage.Guardar(productos,_rutaInventario);
        }
    }
}
