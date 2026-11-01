using ProductosApi.Models;

namespace ProductosApi.Repositories;

// Lista de cosas que se pueden hacer con los productos en la base de datos
public interface IProductoRepository
{
    List<Producto> ObtenerTodos();            // Trae todos los productos
    Producto? ObtenerPorId(int id);           // Trae uno solo (o null si no existe)
    int Insertar(Producto producto);          // Guarda uno nuevo y regresa su Id
    bool Actualizar(Producto producto);       // Cambia sus datos (true si lo encontró)
    bool Eliminar(int id);                    // Lo borra (true si lo encontró)
    List<Producto> BuscarPorNombre(string texto); // Busca productos cuyo nombre contenga el texto
}
