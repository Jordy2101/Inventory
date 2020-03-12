using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventory.Data;
using Inventory.Entity.Warehouse;
using Inventory.Web.Models.Warehouse.Almacen;


namespace Inventory.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlmacensController : ControllerBase
    {
        private readonly DBContextInventory _context;

        public AlmacensController(DBContextInventory context)
        {
            _context = context;
        }

        // GET: api/Almacen/Listar
        [HttpGet]
        public async Task<IEnumerable<Almacen>> Listar()
        {
            var almacen = await _context.almacen.ToListAsync();
            return almacen.Select(c => new Almacen
            {
                ID_ALMACEN = c.ID_ALMACEN,
                DESCRIPCION = c.DESCRIPCION,
                CAPACIDAD = c.CAPACIDAD,
                UBICACION = c.UBICACION,
                ESTADO = c.ESTADO
            }
            );
        }

        // GET: api/Almacen/Mostrar
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> Mostrar([FromRoute]int id)
        {
            var almacen = await _context.almacen.FindAsync(id);

            if (almacen == null)
            {
                return NotFound();
            }

            return Ok(new Almacen
            {
                ID_ALMACEN = almacen.ID_ALMACEN,
                DESCRIPCION = almacen.DESCRIPCION,
                CAPACIDAD = almacen.CAPACIDAD,
                UBICACION = almacen.UBICACION,
                ESTADO = almacen.ESTADO


            });
        }

        // PUT: api/Almacen/Actualiar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] AlmacenViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (model.ID_ALMACEN <= 0)
            {
                return BadRequest();

            }

            var almacen = await _context.almacen.FirstOrDefaultAsync(c => c.ID_ALMACEN == model.ID_ALMACEN);

            if (almacen == null)
            {

                return NotFound();

            }

            almacen.DESCRIPCION = model.DESCRIPCION;
            almacen.CAPACIDAD = model.CAPACIDAD;
            almacen.UBICACION = model.UBICACION;

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


        // POST: api/Almacen/Crear
        [HttpPost("[action]")]
        public async Task<ActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            Almacen almacen = new Almacen
            {
                CAPACIDAD = model.CAPACIDAD,
                DESCRIPCION = model.NOMBRE_ALMACEN,
                UBICACION = model.UBICACION,
                ESTADO = true

            };

            _context.almacen.Add(almacen);
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

        // DELETE: api/Almacen/Eliminar 
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult> Eliminar([FromRoute] int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var almacen = await _context.almacen.FindAsync(id);
            if (almacen == null)
            {

                return NotFound();
            };
            _context.almacen.Remove(almacen);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return Ok(almacen);

        }

        // PUT: api/Almacen/Activar
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var almacen = await _context.almacen.FirstOrDefaultAsync(c => c.ID_ALMACEN == id);

            if (almacen == null)
            {
                return NotFound();
            }

            almacen.ESTADO = true;

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


        //PUT: api/Almacen/Desactivar
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var almacen = await _context.almacen.FirstOrDefaultAsync(c => c.ID_ALMACEN == id);

            if (almacen == null)
            {
                return NotFound();
            }

            almacen.ESTADO = false;

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
            return _context.almacen.Any(e => e.ID_ALMACEN == id);
        }


    }
}