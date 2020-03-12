using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventory.Data;
using Inventory.Entity.Warehouse;
using Inventory.Web.Models.Warehouse.Category; 

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DBContextInventory _context;

        public CategoriesController(DBContextInventory context)
        {
            _context = context;
        }

        // GET: api/Categories/Listar
        [HttpGet]
        public async Task<IEnumerable<Category>> Listar()
        {
            var category = await _context.Categorias.ToListAsync();
            return category.Select(c => new Category
            {
                ID_CATEGORIA = c.ID_CATEGORIA,
                NOMBRE_CATEGORIA = c.NOMBRE_CATEGORIA,
                DESCRIPCION = c.DESCRIPCION,
                ESTADO = c.ESTADO
            }
            ); 
        }

        // GET: api/Categories/Mostrar
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> Mostrar([FromRoute]int id)
        {
            var category = await _context.Categorias.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok ( new Category{

                ID_CATEGORIA = category.ID_CATEGORIA,
                NOMBRE_CATEGORIA = category.NOMBRE_CATEGORIA,
                DESCRIPCION = category.DESCRIPCION,
                ESTADO = category.ESTADO


            });
        }

        // PUT: api/Categories/Actualiar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar ([FromBody] CategoriaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (model.ID_CATEGORIA <= 0)
            {
                return BadRequest();

            }

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.ID_CATEGORIA == model.ID_CATEGORIA);

            if (categoria == null) {

                return NotFound();
            
            }

            categoria.NOMBRE_CATEGORIA = model.NOMBRE_CATEGORIA;
            categoria.DESCRIPCION = model.DESCRIPCION;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok();
        }


        // POST: api/Categories/Crear
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid) {

                return BadRequest(ModelState);
            }

            Category category = new Category
            {
                NOMBRE_CATEGORIA = model.NOMBRE_CATEGORIA,
                DESCRIPCION = model.DESCRIPCION,
                ESTADO = true

            };

            _context.Categorias.Add(category);
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

        // DELETE: api/Categories/Eliminar 
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult> Eliminar([FromRoute] int id)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.Categorias.FindAsync(id);
            if (category == null) {

                return NotFound();
            };
            _context.Categorias.Remove(category);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return Ok(category);

        }

        // PUT: api/Category/Activar
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.ID_CATEGORIA == id);

            if (categoria == null)
            {
                return NotFound();
            }

            categoria.ESTADO = true;

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


        //PUT: api/Category/Desactivar
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.ID_CATEGORIA == id);

            if (categoria == null)
            {
                return NotFound();
            }

            categoria.ESTADO = false;

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

        private bool CategoryExists(int id)
        {
            return _context.Categorias.Any(e => e.ID_CATEGORIA == id);
        }
    }
}
