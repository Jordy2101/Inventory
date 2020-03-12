using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventory.Data;
using Inventory.Entity.Users;
using Inventory.Web.Models.Users; 


namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly DBContextInventory _context;

        public RolesController(DBContextInventory context)
        {
            _context = context;
        }

        // GET: api/Roles/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<RolViewModel>> Listar()
        {
            var rol = await _context.Roles.ToListAsync();

            return rol.Select(r => new RolViewModel
            {
                idrol = r.ID_ROL,
                nombre = r.NOMBRE,
                descripcion = r.DESCRIPCION,
                condicion = r.CONDICION
            });

        }

        // GET: api/Roles/Select
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectViewModel>> Select()
        {
            var rol = await _context.Roles.Where(r => r.CONDICION == true).ToListAsync();

            return rol.Select(r => new SelectViewModel
            {
                idrol = r.ID_ROL,
                nombre = r.NOMBRE
            });
        }


        private bool RolExists(int id)
        {
            return _context.Roles.Any(e => e.ID_ROL == id);
        }
    }
}