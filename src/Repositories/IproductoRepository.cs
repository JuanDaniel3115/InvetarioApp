using InventarioApp.Models;

namespace InventarioApp.Repositories
{
    public interface IproductoRepository
    {
        void Agregar (Producto producto);
        Producto ? ObtenerPorId(int id);
        IEnumerable<Producto> ObtenerTodos();
        bool Eliminar(int id);
        int Cantidad {get;}
    }
}