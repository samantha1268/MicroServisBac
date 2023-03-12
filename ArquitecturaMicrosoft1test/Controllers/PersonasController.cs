using ArquitecturaMicrosoft.Data;
using ArquitecturaMicrosoft.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArquitecturaMicrosoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        public readonly ArquitecturaMicrosoftContext _context;

        public PersonaController(ArquitecturaMicrosoftContext context)
        {
            _context = context;
        }

        // GET: api/Persona
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Persona>>> GetpersonaModel()
        {
            return await _context.Persona.ToListAsync();
        }

        // GET: api/Persona/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> GetPersonaModel(int id)
        {
            var personaModel = await _context.Persona.FindAsync(id);

            if (personaModel == null)
            {
                return NotFound();
            }
            else
            {
                return personaModel;
            }
        }

        //PUT: api/Persona/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonaModel(int id, Persona personaModel)
        {
            if (id != personaModel.IdPesona)
            {
                return BadRequest();
            }

            _context.Entry(personaModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonaModelExists(id))
                {
                    return NotFound("Usuario no encontrado");
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetpersonaModel", new { id = personaModel.IdPesona }, personaModel);
        }

        // POST: api/Persona
        [HttpPost]
        public async Task<ActionResult<Persona>> PostpersonaModel(Persona PersonaModel)
        {
            var Personas = PersonaModel.IdPesona;
            if (!PersonaModelExists(Personas))
            {
                _context.Persona.Add(PersonaModel);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetpersonaModel", new { id = PersonaModel.IdPesona }, PersonaModel);

            }
            else
            {
                return NotFound("Esta persona ya esta registrada");
            }

        }

        // DELETE: api/Persona/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonaModel(int id)
        {
            var personaModel = await _context.Persona.FindAsync(id);
            if (personaModel == null)
            {
                return NotFound("Persona no Encontrada ");
            }

            _context.Persona.Remove(personaModel);
            await _context.SaveChangesAsync();

            return Content("Persona elminado ");
        }

        private bool PersonaModelExists(int id)
        {
            return _context.Persona.Any(e => e.IdPesona == id);
        }

    }
}