using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventory.Data;
using Inventory.Entity.Transactions;
using Inventory.Entity.Transactions.Entrada;
using Inventory.Web.Models.Transactions.Entrada; 


namespace Inventory.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntradasController : ControllerBase
    {

        private readonly DBContextInventory _context;

        public EntradasController(DBContextInventory context)
        {
            _context = context;
        }

        // GET: api/Ingresos/Listar
        [Authorize(Roles = "Almacenero,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<EntradaViewModel>> Listar()
        {
            var entrada = await _context.Entrada
                .Include(i => i.usuario)
                .Include(i => i.persona)
                .OrderByDescending(i => i.ID_ENTRADA)
                .Take(100)
                .ToListAsync();

            return entrada.Select(i => new EntradaViewModel
            {
                ID_ENTRADA = i.ID_ENTRADA,
                ID_PROVEEDOR = i.ID_PROVEEDOR,
                PROVEEDOR = i.persona.NOMBRE,
                ID_USUARIO = i.usuario.ID_USUARIO,
                USUARIO = i.usuario.NOMBRE,
                TIPO_COMPROBANTE = i.TIPO_COMPROBANTE,
                SERIE_COMPROBANTE = i.SERIE_COMPROBANTE,
                NUM_COMPROBANTE = i.NUM_COMPROBANTE,
                FECHA_HORA = i.FECHA_HORA,
                IMPUESTO = i.IMPUESTO,
                TOTAL = i.TOTAL,
                ESTADO = i.ESTADO
            });

        }


        // GET: api/Ingresos/ListarFiltro/texto
        [Authorize(Roles = "Almacenero,Administrador")]
        [HttpGet("[action]/{texto}")]
        public async Task<IEnumerable<EntradaViewModel>> ListarFiltro([FromRoute] string texto)
        {
            var ingreso = await _context.Entrada
                .Include(i => i.usuario)
                .Include(i => i.persona)
                .Where(i => i.NUM_COMPROBANTE.Contains(texto))
                .OrderByDescending(i => i.ID_ENTRADA)
                .ToListAsync();

            return ingreso.Select(i => new EntradaViewModel
            {
                ID_ENTRADA = i.ID_ENTRADA,
                ID_PROVEEDOR = i.ID_PROVEEDOR,
                PROVEEDOR = i.persona.NOMBRE,
                ID_USUARIO = i.ID_USUARIO,
                USUARIO = i.usuario.NOMBRE,
                TIPO_COMPROBANTE = i.TIPO_COMPROBANTE,
                SERIE_COMPROBANTE = i.SERIE_COMPROBANTE,
                NUM_COMPROBANTE = i.NUM_COMPROBANTE,
                FECHA_HORA = i.FECHA_HORA,
                IMPUESTO = i.IMPUESTO,
                TOTAL = i.TOTAL,
                ESTADO = i.ESTADO
            });

        }



        // GET: api/Ingresos/ListarDetalles
        [Authorize(Roles = "Almacenero,Administrador")]
        [HttpGet("[action]/{idingreso}")]
        public async Task<IEnumerable<DetalleViewModel>> ListarDetalles([FromRoute] int idingreso)
        {
            var detalle = await _context.detalle
                .Include(a => a.ID_ARTICULO)
                .Where(d => d.ID_INGRESO == idingreso)
                .ToListAsync();

            return detalle.Select(d => new DetalleViewModel
            {
                ID_ARTICULO = d.ID_ARTICULO,
                articulo = d.articulo.NOMBRE_ARTICULO,
                CANTIDAD = d.CANTIDAD,
                PRECIO = d.PRECIO
            });
            
        }

        // POST: api/Ingresos/Crear
        [Authorize(Roles = "Almacenero,Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fechaHora = DateTime.Now;

            ENTRADA ingreso = new ENTRADA
            {
                ID_PROVEEDOR = model.ID_PROVEEDOR,
                ID_USUARIO = model.ID_USUARIO,
                TIPO_COMPROBANTE = model.TIPO_COMPROBANTE,
                SERIE_COMPROBANTE = model.SERIE_COMPROBANTE,
                NUM_COMPROBANTE = model.NUM_COMPROBANTE,
                FECHA_HORA = fechaHora,
                IMPUESTO = model.IMPUESTO,
                TOTAL = model.TOTAL,
                ESTADO = "Aceptado"
            };


            try
            {
                _context.Entrada.Add(ingreso);
                await _context.SaveChangesAsync();

                var id = ingreso.ID_ENTRADA;
                foreach (var det in model.detalles)
                {
                    DETALLE_ENTRADA detalle = new DETALLE_ENTRADA
                    {
                        ID_INGRESO = id,
                        ID_ARTICULO = det.ID_ARTICULO,
                        CANTIDAD = det.CANTIDAD,
                        PRECIO = det.PRECIO
                    };
                    _context.detalle.Add(detalle);
                    
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }


        private bool IngresoExists(int id)
        {
            return _context.Entrada.Any(e => e.ID_ENTRADA == id);
        }
    }
}
