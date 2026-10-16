using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaOnline.API.Models;
using TiendaOnline.API.Data;

[Route("api/[controller]")]
[ApiController]
public class ProductosController : ControllerBase
{
    private readonly TiendaOnlineDbContext _context;
    public ProductosController(TiendaOnlineDbContext context)
    {
        _context = context;
    }

    // GET: api/Producto
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProducto()
    {
        return await _context.Productos.ToListAsync();
    }

    // GET: api/Producto/5
    [HttpGet("{idproducto}")]
    public async Task<ActionResult<Producto>> GetProducto(int idproducto)
    {
        var producto = await _context.Productos.FindAsync(idproducto);

        if (producto == null)
        {
            return NotFound();
        }

        return producto;
    }

    // PUT: api/Producto/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{idproducto}")]
    public async Task<IActionResult> PutProducto(int? idproducto, Producto producto)
    {
        if (idproducto != producto.IdProducto)
        {
            return BadRequest();
        }

        _context.Entry(producto).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductoExists(idproducto))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Producto
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Producto>> PostProducto(Producto producto)
    {
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProducto", new { idproducto = producto.IdProducto }, producto);
    }

    // DELETE: api/Producto/5
    [HttpDelete("{idproducto}")]
    public async Task<IActionResult> DeleteProducto(int? idproducto)
    {
        var producto = await _context.Productos.FindAsync(idproducto);
        if (producto == null)
        {
            return NotFound();
        }

        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductoExists(int? idproducto)
    {
        return _context.Productos.Any(e => e.IdProducto == idproducto);
    }
}
