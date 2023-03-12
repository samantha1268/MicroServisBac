using ArquitecturaMicrosoft.Data;
using ArquitecturaMicrosoft.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ArquitecturaMicrosoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ArquitecturaMicrosoftContext _context;

        public ClienteController(ArquitecturaMicrosoftContext context)
        {
            _context = context;
        }
        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetClienteModel(int id)
        {
            var ClienteModel = await _context.Cliente.FindAsync(id);
            //var ClienteModel = await _context.Cliente.FindAsync(id);
            //var Persona = new Cliente() { Clienteid = 34245, contraseña = "1268", estado = "true", IdPersona = 34245 });

            if (ClienteModel == null)
            {
                return NotFound("Cliente no Existe");
            }
            else
            { 
                
                return ClienteModel;
            }
        }
        // GET: api/Cliente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClienteModel()
        {
            return await _context.Cliente.ToListAsync();
        }

        // PUT: api/Cliente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClienteModel(int id, Cliente clienteModel)
        {
            if (id != clienteModel.Clienteid)
            {
                return BadRequest();
            }

            _context.Entry(clienteModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("PostClienteModel", new { id = clienteModel.Clienteid }, clienteModel);
        }

        // POST: api/Cliente
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostClienteModel(Cliente clienteModel)
        {
            var cliente = clienteModel.Clienteid;
            if (!PersonaExists(cliente))
            {
                return NotFound("Cliente ya Existe");
            }
            else
            {
                _context.Cliente.Add(clienteModel);
                await _context.SaveChangesAsync();
                return CreatedAtAction("PostClienteModel", new { id = clienteModel.Clienteid }, clienteModel);
            }
        }

        // DELETE: api/Persona/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClienteModel(int id)
        {
            var clienteModel = await _context.Cliente.FindAsync(id);
            if (clienteModel == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(clienteModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool ClienteModelExists(int id)
        {
            return _context.Cliente.Any(e => e.Clienteid == id);
        }
        private bool PersonaExists(int id)
        {
            return _context.Persona.Any(e => e.IdPesona == id);
        }

    }
}
