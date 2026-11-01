using MySqlConnector;
using ProductosApi.Models;

namespace ProductosApi.Repositories;

public class ProductoRepository : IProductoRepository
{
    private readonly string _cadenaConexion;

    // Aquí tomamos la cadena de conexión que escribimos en appsettings.json
    public ProductoRepository(IConfiguration configuration)
    {
        _cadenaConexion = configuration.GetConnectionString("Default")
            ?? throw new InvalidOperationException("Falta la cadena de conexión 'Default'");
    }

    // CONEXIÓN: crea una conexión nueva y la abre.
    // Quien la pida debe cerrarla (lo hacemos con "using").
    private MySqlConnection CrearConexion()
    {
        var conexion = new MySqlConnection(_cadenaConexion);
        conexion.Open();
        return conexion;
    }

    // Convierte la fila que estamos leyendo en un objeto Producto
    private static Producto LeerProducto(MySqlDataReader reader)
    {
        return new Producto
        {
            Id = reader.GetInt32("id"),
            Nombre = reader.GetString("nombre"),
            Descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion"))
                ? null
                : reader.GetString("descripcion"),
            Precio = reader.GetDecimal("precio"),
            Stock = reader.GetInt32("stock")
        };
    }

    // CONSULTA: trae todos los productos
    public List<Producto> ObtenerTodos()
    {
        var lista = new List<Producto>();

        using var conexion = CrearConexion();
        using var comando = new MySqlCommand(
            "SELECT id, nombre, descripcion, precio, stock FROM productos ORDER BY id", conexion);
        using var reader = comando.ExecuteReader();

        while (reader.Read())
            lista.Add(LeerProducto(reader));

        return lista;
    }

    // CONSULTA: trae un producto por su Id
    public Producto? ObtenerPorId(int id)
    {
        using var conexion = CrearConexion();
        using var comando = new MySqlCommand(
            "SELECT id, nombre, descripcion, precio, stock FROM productos WHERE id = @id", conexion);
        comando.Parameters.AddWithValue("@id", id);
        using var reader = comando.ExecuteReader();

        // Si hay una fila la regresamos, si no, regresamos null
        return reader.Read() ? LeerProducto(reader) : null;
    }

    // INSERT: guarda un producto nuevo y regresa el Id que le dio la base de datos
    public int Insertar(Producto producto)
    {
        using var conexion = CrearConexion();
        using var comando = new MySqlCommand(
            @"INSERT INTO productos (nombre, descripcion, precio, stock)
              VALUES (@nombre, @descripcion, @precio, @stock);
              SELECT LAST_INSERT_ID();", conexion);

        comando.Parameters.AddWithValue("@nombre", producto.Nombre);
        comando.Parameters.AddWithValue("@descripcion", (object?)producto.Descripcion ?? DBNull.Value);
        comando.Parameters.AddWithValue("@precio", producto.Precio);
        comando.Parameters.AddWithValue("@stock", producto.Stock);

        return Convert.ToInt32(comando.ExecuteScalar());
    }

    // UPDATE: cambia los datos de un producto que ya existe
    public bool Actualizar(Producto producto)
    {
        using var conexion = CrearConexion();
        using var comando = new MySqlCommand(
            @"UPDATE productos
              SET nombre = @nombre, descripcion = @descripcion, precio = @precio, stock = @stock
              WHERE id = @id", conexion);

        comando.Parameters.AddWithValue("@id", producto.Id);
        comando.Parameters.AddWithValue("@nombre", producto.Nombre);
        comando.Parameters.AddWithValue("@descripcion", (object?)producto.Descripcion ?? DBNull.Value);
        comando.Parameters.AddWithValue("@precio", producto.Precio);
        comando.Parameters.AddWithValue("@stock", producto.Stock);

        // Si se modificó al menos una fila, sí existía el producto
        return comando.ExecuteNonQuery() > 0;
    }

    // DELETE: borra un producto
    public bool Eliminar(int id)
    {
        using var conexion = CrearConexion();
        using var comando = new MySqlCommand("DELETE FROM productos WHERE id = @id", conexion);
        comando.Parameters.AddWithValue("@id", id);

        return comando.ExecuteNonQuery() > 0;
    }

    // BUSCAR: trae los productos que tengan el texto en el nombre (ej. "mou" encuentra "Mouse")
    public List<Producto> BuscarPorNombre(string texto)
    {
        var lista = new List<Producto>();

        using var conexion = CrearConexion();
        using var comando = new MySqlCommand(
            @"SELECT id, nombre, descripcion, precio, stock
              FROM productos
              WHERE nombre LIKE @texto
              ORDER BY nombre", conexion);
        comando.Parameters.AddWithValue("@texto", $"%{texto}%");
        using var reader = comando.ExecuteReader();

        while (reader.Read())
            lista.Add(LeerProducto(reader));

        return lista;
    }
}
