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
using Inventory.Entity.Transactions.Salida;
using Inventory.Web.Models.Transactions.Salida;

namespace Inventory.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalidasController : ControllerBase
    {
        private readonly DBContextInventory _context;

        public SalidasController(DBContextInventory context)
        {
            _context = context;
        }

        // GET: api/Ventas/Listar
        [Authorize(Roles = "Vendedor,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<SalidaViewModel>> Listar()
        {
            var venta = await _context.salida
                .Include(v => v.usuario)
                .Include(v => v.persona)
                .OrderByDescending(v => v.ID_SALIDA)
                .Take(100)
                .ToListAsync();

            return venta.Select(v => new SalidaViewModel
            {
                idventa = v.ID_SALIDA,
                idcliente = v.ID_CLIENTE,
                cliente = v.persona.NOMBRE,
                idusuario = v.ID_USUARIO,
                usuario = v.usuario.NOMBRE,
                tipo_comprobante = v.TIPO_COMPROBANTE,
                serie_comprobante = v.SERIE_COMPROBANTE,
                num_comprobante = v.NUM_COMPROBANTE,
                fecha_hora = v.FECHA_HORA,
                impuesto = v.IMPUESTO,
                total = v.TOTAL,
                estado = v.ESTADO
            });

        }


        // GET: api/Ventas/ListarFiltro/texto
        [Authorize(Roles = "Vendedor,Administrador")]
        [HttpGet("[action]/{texto}")]
        public async Task<IEnumerable<SalidaViewModel>> ListarFiltro([FromRoute] string texto)
        {
            var venta = await _context.salida
                .Include(v => v.usuario)
                .Include(v => v.persona)
                .Where(v => v.NUM_COMPROBANTE.Contains(texto))
                .OrderByDescending(v => v.ID_SALIDA)
                .ToListAsync();

            return venta.Select(v => new SalidaViewModel
            {
                idventa = v.ID_SALIDA,
                idcliente = v.ID_CLIENTE,
                cliente = v.persona.NOMBRE,
                idusuario = v.ID_USUARIO,
                usuario = v.usuario.NOMBRE,
                tipo_comprobante = v.TIPO_COMPROBANTE,
                serie_comprobante = v.SERIE_COMPROBANTE,
                num_comprobante = v.NUM_COMPROBANTE,
                fecha_hora = v.FECHA_HORA,
                impuesto = v.IMPUESTO,
                total = v.TOTAL,
                estado = v.ESTADO
            });

        }



        // GET: api/Ventas/ListarDetalles
        [Authorize(Roles = "Vendedor,Administrador")]
        [HttpGet("[action]/{idventa}")]
        public async Task<IEnumerable<DetalleViewModel>> ListarDetalles([FromRoute] int idventa)
        {
            var detalle = await _context.detallesalida
                .Include(a => a.articulo)
                .Where(d => d.idventa == idventa)
                .ToListAsync();

            return detalle.Select(d => new DetalleViewModel
            {
                idarticulo = d.idarticulo,
                articulo = d.articulo.NOMBRE_ARTICULO,
                cantidad = d.cantidad,
                precio = d.precio,
                descuento = d.descuento
            });

        }

        // POST: api/Ventas/Crear
        [Authorize(Roles = "Vendedor,Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var fechaHora = DateTime.Now;

            SALIDA salida = new SALIDA
            {
                ID_CLIENTE = model.idcliente,
                ID_USUARIO = model.idusuario,
                TIPO_COMPROBANTE = model.tipo_comprobante,
                SERIE_COMPROBANTE = model.serie_comprobante,
                NUM_COMPROBANTE = model.num_comprobante,
                FECHA_HORA = fechaHora,
                IMPUESTO = model.impuesto,
                TOTAL = model.total,
                ESTADO = "Aceptado"
            };


            try
            {
                _context.salida.Add(salida);
                await _context.SaveChangesAsync();

                var id = salida.ID_SALIDA;
                foreach (var det in model.detalles)
                {
                    DETALLE_SALIDA detalle = new DETALLE_SALIDA
                    {
                        idventa = id,
                        idarticulo = det.idarticulo,
                        cantidad = det.cantidad,
                        precio = det.precio,
                        descuento = det.descuento
                    };
                    _context.detallesalida.Add(detalle);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Ventas/Anular/1
        [Authorize(Roles = "Vendedor,Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Anular([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var venta = await _context.salida.FirstOrDefaultAsync(v => v.ID_SALIDA == id);

            if (venta == null)
            {
                return NotFound();
            }

            venta.ESTADO = "Anulado";

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


        private bool VentaExists(int id)
        {
            return _context.salida.Any(e => e.ID_SALIDA == id);
        }






    }
}