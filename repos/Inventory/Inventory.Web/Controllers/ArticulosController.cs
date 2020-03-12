using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventory.Data;
using Inventory.Entity.Warehouse;
using Inventory.Web.Models.Warehouse.Articulo; 

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly DBContextInventory _context;

        public ArticulosController(DBContextInventory context)
        {
            _context = context;
        }

        // GET: api/Articulos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ArticuloViewModel>> Listar()
        {
            var articulo = await _context.Articulos.Include(a => a.category ).ToListAsync();
           

            return articulo.Select(a => new ArticuloViewModel
            {
                idarticulo = a.ID_ARTICULO,
                idcategoria = a.ID_CATEGORIA,
                idalmacen = a.ID_ALMACEN,
                category = a.category.DESCRIPCION,
                nombre = a.NOMBRE_ARTICULO,
                stock = a.STOCK,
                precio_venta = a.PRECIO,
                descripcion = a.DESCRIPCION,
                condicion = a.ESTADO
            });

        }


        // GET: api/Articulos/ListarIngreso/texto
        [Authorize(Roles = "Almacenero,Administrador")]
        [HttpGet("[action]/{texto}")]
        public async Task<IEnumerable<ArticuloViewModel>> ListarIngreso([FromRoute] string texto)
        {
            var articulo = await _context.Articulos.Include(a => a.category)
                .Where(a => a.NOMBRE_ARTICULO.Contains(texto))
                .Where(a => a.ESTADO == true)
                .ToListAsync();

            return articulo.Select(a => new ArticuloViewModel
            {
                idarticulo = a.ID_ARTICULO,
                idcategoria = a.ID_CATEGORIA,
                category = a.category.NOMBRE_CATEGORIA,
                nombre = a.NOMBRE_ARTICULO,
                stock = a.STOCK,
                precio_venta = a.PRECIO,
                descripcion = a.DESCRIPCION,
                condicion = a.ESTADO
            });

        }

        // GET: api/Articulos/ListarVenta/texto
        [Authorize(Roles = "Vendedor,Administrador")]
        [HttpGet("[action]/{texto}")]
        public async Task<IEnumerable<ArticuloViewModel>> ListarVenta([FromRoute] string texto)
        {
            var articulo = await _context.Articulos.Include(a => a.category)
                .Where(a => a.NOMBRE_ARTICULO.Contains(texto))
                .Where(a => a.ESTADO == true)
                .Where(a => a.STOCK > 0)
                .ToListAsync();

            return articulo.Select(a => new ArticuloViewModel
            {
                idarticulo = a.ID_ARTICULO,
                idcategoria = a.ID_CATEGORIA,
                category = a.category.NOMBRE_CATEGORIA,
                nombre = a.NOMBRE_ARTICULO,
                stock = a.STOCK,
                precio_venta = a.PRECIO,
                descripcion = a.DESCRIPCION,
                condicion = a.ESTADO
            });

        }


        // GET: api/Articulos/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var articulo = await _context.Articulos.Include(a => a.category).
                SingleOrDefaultAsync(a => a.ID_ARTICULO == id);

            if (articulo == null)
            {
                return NotFound();
            }

            return Ok(new ArticuloViewModel
            {
                idarticulo = articulo.ID_ARTICULO,
                idcategoria = articulo.ID_CATEGORIA,
                category = articulo.category.DESCRIPCION,
                nombre = articulo.NOMBRE_ARTICULO,
                descripcion = articulo.DESCRIPCION,
                stock = articulo.STOCK,
                precio_venta = articulo.PRECIO,
                condicion = articulo.ESTADO
            });
        }

        // PUT: api/Articulos/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idarticulo <= 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.ID_ARTICULO == model.idarticulo);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.ID_CATEGORIA = model.idcategoria;
            articulo.NOMBRE_ARTICULO = model.nombre;
            articulo.PRECIO = model.precio_venta;
            articulo.STOCK = model.stock;
            articulo.DESCRIPCION = model.descripcion;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }

        // POST: api/Articulos/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Articulo articulo = new Articulo
            {
                ID_CATEGORIA = model.idcategoria,
                NOMBRE_ARTICULO = model.nombre,
                PRECIO = model.precio_venta,
                STOCK = model.stock,
                DESCRIPCION = model.descripcion,
                ESTADO = true
            };

            _context.Articulos.Add(articulo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Articulos/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.ID_ARTICULO == id);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.ESTADO = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Articulos/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.ID_ARTICULO == id);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.ESTADO = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }


        private bool ArticuloExists(int id)
        {
            return _context.Articulos.Any(e => e.ID_ARTICULO == id);
        }
    }
}