using ArquitecturaMicrosoft.Data;
using ArquitecturaMicrosoft.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ArquitecturaMicrosoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly ArquitecturaMicrosoftContext _context;

        public CuentaController(ArquitecturaMicrosoftContext context)
        {
            _context = context;
        }

        // GET: api/Cuenta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cuenta>>> GetCuentaModelo()
        {
            return await _context.Cuenta.ToListAsync();
        }
        // GET: api/Cuenta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cuenta>> GetcuentaModel(int id)
        {
            var CuentaModel = await _context.Cuenta.FindAsync(id);

            if (CuentaModel == null)
            {
                return NotFound("Cuenta no existente");
            }
            else
            {
                return CuentaModel;
            }
        }
        // PUT: api/Cuenta/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuentaModelo(int id, Cuenta cuentaModelo)
        {
            if (id != cuentaModelo.IdNúmeroCuenta)
            {
                return BadRequest();
            }

            _context.Entry(cuentaModelo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentaModeloExists(id))
                {
                    return NotFound(); 
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCuentaModelo", new { id = cuentaModelo.IdNúmeroCuenta }, cuentaModelo);
        }

        // POST: api/Cuenta
        [HttpPost]
        public async Task<ActionResult<Cuenta>> PostCuentaModelo(Cuenta cuentaModelo)
        {
            var cliente = cuentaModelo.idcliente;
            if (!PersonaExists(cliente))
            {
                return NotFound();
            }
            else
            {
                if (!ClienteExist(cliente))
                {
                    return NotFound();
                }
                else
                {
                    _context.Cuenta.Add(cuentaModelo);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetCuentaModelo", new { id = cuentaModelo.IdNúmeroCuenta }, cuentaModelo);
                }
            }
        }

        // DELETE: api/Cuenta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuentaModelo(int id)
        {
            var cuentaModelo = await _context.Cuenta.FindAsync(id);
            if (cuentaModelo == null)
            {
                return Ok("Cuenta no existente");
            }

            _context.Cuenta.Remove(cuentaModelo);
            await _context.SaveChangesAsync();

            return Content("Cuenta elminada ");
        }

        private bool CuentaModeloExists(int id)
        {
            return _context.Cuenta.Any(e => e.IdNúmeroCuenta == id);
        }
        private bool ClienteExist(int id)
        {
            return _context.Cliente.Any(c => c.Clienteid == id);
        }
        private bool PersonaExists(int id)
        {
            return _context.Persona.Any(e => e.IdPesona == id);
        }
    }
}
