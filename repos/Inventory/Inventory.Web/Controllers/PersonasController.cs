using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventory.Data;
using Inventory.Entity.Users;
using Inventory.Web.Models.Users.Persona; 


namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly DBContextInventory _context;

        public PersonasController(DBContextInventory context)
        {
            _context = context;
        }

        // GET: api/Personas/ListarClientes
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonaViewModel>> ListarClientes()
        {
            var persona = await _context.Persona.Where(p => p.TIPO_PERSONA == "Cliente").ToListAsync();

            return persona.Select(p => new PersonaViewModel
            {
                idpersona = p.ID_PERSONA,
                tipo_persona = p.TIPO_PERSONA,
                nombre = p.NOMBRE,
                tipo_documento = p.TIPO_DOCUMENTO,
                num_documento = p.NUM_DOCUMENTO,
                direccion = p.DIRECCION,
                telefono = p.TELEFONO,
                email = p.EMAIL
            });

        }

        // GET: api/Personas/ListarProveedores
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonaViewModel>> ListarProveedores()
        {
            var persona = await _context.Persona.Where(p => p.TIPO_PERSONA == "Proveedor").ToListAsync();

            return persona.Select(p => new PersonaViewModel
            {
                idpersona = p.ID_PERSONA,
                tipo_persona = p.TIPO_PERSONA,
                nombre = p.NOMBRE,
                tipo_documento = p.TIPO_DOCUMENTO,
                num_documento = p.NUM_DOCUMENTO,
                direccion = p.DIRECCION,
                telefono = p.TELEFONO,
                email = p.EMAIL
            });

        }

        // POST: api/Personas/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = model.email.ToLower();

            if (await _context.Persona.AnyAsync(p => p.EMAIL == email))
            {
                return BadRequest("El email ya existe");
            }

            PERSONA persona = new PERSONA
            {
                TIPO_PERSONA = model.tipo_persona,
                NOMBRE = model.nombre,
                TIPO_DOCUMENTO = model.tipo_documento,
                NUM_DOCUMENTO = model.num_documento,
                DIRECCION = model.direccion,
                TELEFONO = model.telefono,
                EMAIL = model.email.ToLower()
            };

            _context.Persona.Add(persona);
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

        // PUT: api/Personas/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idpersona <= 0)
            {
                return BadRequest();
            }

            var persona = await _context.Persona.FirstOrDefaultAsync(p => p.ID_PERSONA == model.idpersona);

            if (persona == null)
            {
                return NotFound();
            }

            persona.TIPO_PERSONA = model.tipo_persona;
            persona.NOMBRE = model.nombre;
            persona.TIPO_DOCUMENTO = model.tipo_documento;
            persona.NUM_DOCUMENTO = model.num_documento;
            persona.DIRECCION = model.direccion;
            persona.TELEFONO = model.telefono;
            persona.EMAIL = model.email.ToLower();

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


        private bool PersonaExists(int id)
        {
            return _context.Persona.Any(e => e.ID_PERSONA == id);
        }
    }
}