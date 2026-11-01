using Microsoft.AspNetCore.Mvc;
using ProductosApi.Models;
using ProductosApi.Repositories;

namespace ProductosApi.Controllers;

// Aquí están las direcciones (endpoints) que se ven en Swagger
[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IProductoRepository _repo;

    public ProductosController(IProductoRepository repo)
    {
        _repo = repo;
    }

    // GET api/productos -> lista todos
    [HttpGet]
    public ActionResult<List<Producto>> ObtenerTodos()
    {
        return Ok(_repo.ObtenerTodos());
    }

    // GET api/productos/5 -> trae uno por Id
    [HttpGet("{id:int}")]
    public ActionResult<Producto> ObtenerPorId(int id)
    {
        var producto = _repo.ObtenerPorId(id);
        if (producto == null)
            return NotFound($"No existe un producto con id {id}");

        return Ok(producto);
    }

    // GET api/productos/buscar?nombre=mouse -> busca por nombre
    [HttpGet("buscar")]
    public ActionResult<List<Producto>> Buscar([FromQuery] string nombre)
    {
        return Ok(_repo.BuscarPorNombre(nombre));
    }

    // POST api/productos -> crea uno nuevo
    [HttpPost]
    public ActionResult<Producto> Insertar(Producto producto)
    {
        producto.Id = _repo.Insertar(producto);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = producto.Id }, producto);
    }

    // PUT api/productos/5 -> modifica uno existente
    [HttpPut("{id:int}")]
    public IActionResult Actualizar(int id, Producto producto)
    {
        producto.Id = id;
        if (!_repo.Actualizar(producto))
            return NotFound($"No existe un producto con id {id}");

        return NoContent();
    }

    // DELETE api/productos/5 -> elimina uno
    [HttpDelete("{id:int}")]
    public IActionResult Eliminar(int id)
    {
        if (!_repo.Eliminar(id))
            return NotFound($"No existe un producto con id {id}");

        return NoContent();
    }
}
