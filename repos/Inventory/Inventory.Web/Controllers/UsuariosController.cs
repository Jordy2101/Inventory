using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Inventory.Data;
using Inventory.Entity.Users;
using Inventory.Web.Models.Users.Usuarios;
using Inventory.Web.Models.Users.Login;


namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly DBContextInventory _context;
        private readonly IConfiguration _config;

        public UsuariosController(DBContextInventory context)
        {
            _context = context;
        }

        // GET: api/Usuarios/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<UsuarioViewModel>> Listar()
        {
            var usuario = await _context.Usuarios.Include(u => u.rol).ToListAsync();

            return usuario.Select(u => new UsuarioViewModel
            {
                idusuario = u.ID_USUARIO,
                idrol = u.ID_ROL,
                rol = u.rol.NOMBRE,
                nombre = u.NOMBRE,
                tipo_documento = u.TIPO_OCUMENTO,
                num_documento = u.NUM_DOCUMENTO,
                direccion = u.DIRECCION,
                telefono = u.TELEFONO,
                email = u.EMAIL,
                password_hash = u.PASSWORD_HAS,
                condicion = u.CONDICION
            });

        }

        // POST: api/Usuarios/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = model.email.ToLower();

            if (await _context.Usuarios.AnyAsync(u => u.EMAIL == email))
            {
                return BadRequest("El email ya existe");
            }

            CrearPasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);

            USUARIO usuario = new USUARIO
            {
                ID_ROL = model.idrol,
                NOMBRE = model.nombre,
                TIPO_OCUMENTO = model.tipo_documento,
                NUM_DOCUMENTO = model.num_documento,
                DIRECCION = model.direccion,
                TELEFONO = model.telefono,
                EMAIL = model.email.ToLower(),
                PASSWORD_HAS = passwordHash,
                PASSWORD_SALT = passwordSalt,
                CONDICION = true
            };

            _context.Usuarios.Add(usuario);
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


        // PUT: api/Articulos/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idusuario <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.ID_USUARIO == model.idusuario);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.ID_USUARIO = model.idrol;
            usuario.NOMBRE = model.nombre;
            usuario.TIPO_OCUMENTO = model.tipo_documento;
            usuario.NUM_DOCUMENTO = model.num_documento;
            usuario.DIRECCION = model.direccion;
            usuario.TELEFONO = model.telefono;
            usuario.EMAIL = model.email.ToLower();

            if (model.act_password == true)
            {
                CrearPasswordHash(model.password, out byte[] passwordHash, out byte[] passwordSalt);
                usuario.PASSWORD_HAS = passwordHash;
                usuario.PASSWORD_SALT = passwordSalt;
            }

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

        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        // PUT: api/Usuarios/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.ID_USUARIO == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.CONDICION = false;

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

        // PUT: api/Usuarios/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.ID_USUARIO == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.CONDICION = true;

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

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var email = model.email.ToLower();

            var usuario = await _context.Usuarios.Where(u => u.CONDICION == true).Include(u => u.rol).FirstOrDefaultAsync(u => u.EMAIL == email);

            if (usuario == null)
            {
                return NotFound();
            }

            if (!VerificarPasswordHash(model.password, usuario.PASSWORD_HAS, usuario.PASSWORD_SALT))
            {
                return NotFound();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.ID_USUARIO.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, usuario.rol.NOMBRE ),
                new Claim("ID_USUARIO", usuario.ID_USUARIO.ToString() ),
                new Claim("ROL", usuario.rol.NOMBRE ),
                new Claim("NOMBRE", usuario.NOMBRE )
            };

            return Ok(
                    new { token = GenerarToken(claims) }
                );

        }

        private bool VerificarPasswordHash(string password, byte[] passwordHashAlmacenado, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var passwordHashNuevo = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return new ReadOnlySpan<byte>(passwordHashAlmacenado).SequenceEqual(new ReadOnlySpan<byte>(passwordHashNuevo));
            }
        }

        private string GenerarToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              _config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds,
              claims: claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.ID_USUARIO == id);
        }
    }
}